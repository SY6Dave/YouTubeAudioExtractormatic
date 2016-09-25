using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoLibrary;

namespace YouTubeAudioExtractormatic
{
    public class Downloader : INotifyPropertyChanged
    {
        #region PropertyChangedEvents

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnDownloadProgressChanged(EventArgs e)
        {
            EventHandler handler = DownloadProgressChanged;
            if (handler != null)
                handler(this, e);

            Debug.WriteLine(downloadProgress); //log current download percentage
           // gui.UpdateMsgLbl(downloadProgress + "% downloaded..."); FIX WITH REFACTOR
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DownloadProgressChanged;

        #endregion

        ThreadHandler threadHandler;
        private List<Thread> downloadThreads;
        static string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
        string downloadsPath = Path.Combine(applicationPath, "Downloads");
        public string DownloadsPath
        {
            get { return downloadsPath; }
        }
        string ffmpegPath = Path.Combine(applicationPath, "lib\\ffmpeg.exe");
        private int downloadProgress;
        public int DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                if(value != downloadProgress)
                {
                    downloadProgress = value;
                    OnPropertyChanged("DownloadProgress");
                    OnDownloadProgressChanged(EventArgs.Empty);
                }
            }
        }

        private iGui gui; //to replace frmmain

        private List<VideoData> pendingDownloads;
        object downloadLocker = new object();

        /// <summary>
        /// Verifies that there is a directory set up for downloads
        /// </summary>
        /// <param name="threadHandler">The main program's thread manager</param>
        public Downloader(ThreadHandler threadHandler, iGui callingForm)
        {
            this.threadHandler = threadHandler;
            this.gui = callingForm;
            this.pendingDownloads = new List<VideoData>();
            
            this.downloadThreads = new List<Thread>();
            for (int i = 0; i < 4; i++)
            {
                Thread download = new Thread(WaitForDownload);
                downloadThreads.Add(download);
                threadHandler.AddActive(download);
                download.Start();
            }

                //check ffmpeg in right place
                if (!File.Exists(ffmpegPath))
                {
                    //gui.UpdateMsgLbl("ffmpeg.exe not found in lib folder!"); FIX WITH REFACTOR
                }

            //check if downloads folder is there and try to create it if not
            if(!Directory.Exists(downloadsPath))
            {
                try
                {
                    Directory.CreateDirectory(downloadsPath);
                }
                catch
                {
                    //gui.UpdateMsgLbl("Unable to create downloads directory!"); FIX WITH REFACTOR
                }
            }
        }

        private void WaitForDownload()
        {
            for (; ; )
            {
                Debug.WriteLine(Thread.CurrentThread.ManagedThreadId + " thread waiting for download");
                try
                {
                    VideoData video;
                    lock (downloadLocker)
                    {
                        while (pendingDownloads.Count == 0)
                        {
                            Thread.Sleep(10);
                        }
                        Debug.WriteLine(Thread.CurrentThread.ManagedThreadId + " thread found pending download");
                        video = pendingDownloads.First();
                        pendingDownloads.Remove(video);
                    }
                    Download(video);
                }
                catch (ThreadAbortException)
                {
                    return;
                }
            }
        }

        public void SetPendingDownloads(List<VideoData> videoCollection, uint bitrate)
        {
            foreach (var video in videoCollection)
            {
                UrlParser urlParser = new UrlParser(video.Url);
                video.Url = urlParser.Url;
                video.SetDesiredBirtate(bitrate);

                pendingDownloads.Add(video);
            }
        }

        private void Download(VideoData video)
        {
            using (var cli = Client.For(new YouTube())) //use a libvideo client to get video metadata
            {
                IEnumerable<YouTubeVideo> downloadLinks = null;

                try
                {
                    downloadLinks = cli.GetAllVideos(video.Url).OrderBy(br => -br.AudioBitrate); //sort by highest audio quality
                }
                catch (ArgumentException)
                {
                    video.DownloadFailed = true;
                    //invalid url
                    //gui.UpdateMsgLbl("Invalid URL!"); FIX WITH REFACTOR

                    threadHandler.RemoveActive(Thread.CurrentThread);
                    Thread.CurrentThread.Abort();
                }

                YouTubeVideo highestQuality = null;

                try
                {
                    highestQuality = downloadLinks.First(); //grab best quality link
                }
                catch
                {
                    video.DownloadFailed = true;
                    //gui.UpdateMsgLbl("Unable to download video"); FIX WITH REFACTOR
                    return;
                }

                //setup http web request to get video bytes
                var request = (HttpWebRequest)HttpWebRequest.Create(highestQuality.Uri);
                request.AllowAutoRedirect = true;
                request.Method = "GET";
                request.Proxy = HttpWebRequest.DefaultWebProxy;
                request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

                //execute request and save bytes to buffer
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var len = response.ContentLength;
                    var buffer = new byte[256];
                    using (var stream = response.GetResponseStream())
                    {
                        stream.ReadTimeout = 5000;
                        using (var tempbytes = new TempFile())
                        {
                            FileStream bytes = new FileStream(tempbytes.Path, FileMode.Open);
                            while (bytes.Length < len)
                            {
                                try
                                {
                                    var read = stream.Read(buffer, 0, buffer.Length);
                                    if (read > 0)
                                    {
                                       bytes.Write(buffer, 0, read);
                                       video.SetDownloadProgress((bytes.Length * 100 / len));
                                       gui.OnProgressChanged();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    video.DownloadFailed = true;
                                    //thread aborted
                                    return;
                                }
                            }

                            //check integrity of byte array
                            if (bytes.Length != len)
                            {
                                video.DownloadFailed = true;
                               // gui.UpdateMsgLbl("File content is corrupted!"); FIX WITH REFACTOR
                                threadHandler.RemoveActive(Thread.CurrentThread);
                                Thread.CurrentThread.Abort();
                            }
                            else
                            {
                                if (video.DesiredBitrate == 0) //video
                                {
                                    video.SetConvertProgress(100);
                                    string videoPath = Path.Combine(downloadsPath, highestQuality.FullName);
                                    File.Copy(tempbytes.Path, videoPath, true);

                                    //gui.UpdateMsgLbl("Successful!"); FIX WITH REFACTOR
                                }
                                else //mp3
                                {
                                    //create temp video file to convert to mp3 and dispose of when done
                                    //File.WriteAllBytes(tempVideo.Path, bytes.ToArray());
                                    TimeSpan duration = GetVideoDuration(tempbytes.Path);
                                    string audioPath = Path.Combine(downloadsPath, highestQuality.FullName + ".mp3");

                                    ToMp3(tempbytes.Path, audioPath, duration, video, video.DesiredBitrate); //convert to mp3
                                }
                            }

                            bytes.Dispose();
                            bytes.Close();
                        }
                    }
                }
            }

            WaitForDownload(); //thread goes back to waiting for another download to begin
        }

        /// <summary>
        /// Convert a video file to an mp3 of a desired bitrate
        /// </summary>
        /// <param name="videoPath">The file path of the video</param>
        /// <param name="audioPath">The file path to save the mp3</param>
        /// /// <param name="duration">The duration of the video file. Used to calculate progress</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        /// <returns>Returns true if the conversion was successful</returns>
        private bool ToMp3(string videoPath, string audioPath, TimeSpan duration, VideoData videoData, uint bitrate = 320)
        {
            //setup an ffmpeg process
            var ffmpeg = new Process
            {
                StartInfo = { UseShellExecute = false, RedirectStandardError = true, FileName = ffmpegPath , CreateNoWindow = true}
            };

            var arguments =
                String.Format(
                    @"-y -i ""{0}"" -b:a {1}K -vn ""{2}""",
                    videoPath,
                    bitrate,
                    audioPath
                );

            ffmpeg.StartInfo.Arguments = arguments;

            try
            {
                //try to invoke ffmpeg
                if (!ffmpeg.Start())
                {
                    videoData.DownloadFailed = true;
                    Debug.WriteLine("Unable to start ffmpeg!");
                    return false;
                }
                var reader = ffmpeg.StandardError;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //get the line saying the time - use this to calculate percentage
                    if(line.Contains("time="))
                    {
                        string[] lineSplit = line.Split(' ');
                        foreach(string part in lineSplit)
                        {
                            if(part.Contains("time="))
                            {
                                TimeSpan timeConverted = TimeSpan.Parse(part.Replace("time=", ""));
                                double percentage = ((double)timeConverted.Ticks / (double)duration.Ticks) * 100;
                                videoData.SetConvertProgress(percentage);
                                gui.OnProgressChanged();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                videoData.DownloadFailed = true;
                return false; //exception was thrown, conversion failed
            }

            ffmpeg.Close();

            return true;
        }

        /// <summary>
        /// Get the duration of a given video
        /// </summary>
        /// <param name="videoPath">The path of the video file</param>
        /// <returns></returns>
        private TimeSpan GetVideoDuration(string videoPath)
        {
            //setup an ffmpeg process
            var ffmpeg = new Process
            {
                StartInfo = { UseShellExecute = false, RedirectStandardError = true, FileName = ffmpegPath }
            };

            var arguments =
                String.Format(
                    @"-i ""{0}""",
                    videoPath
                );

            ffmpeg.StartInfo.Arguments = arguments;

            try
            {
                //try to invoke ffmpeg
                if (!ffmpeg.Start())
                {
                    return TimeSpan.Zero;
                }
                var reader = ffmpeg.StandardError;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //get the line which shows the duration, parse it and return
                    if (line.Contains("Duration: "))
                    {
                        string[] lineSplit = line.Split(' ');
                        for (int i = 0; i < lineSplit.Length; i++)
                        {
                            string part = lineSplit[i];
                            if (part.Contains("Duration:"))
                            {
                                part = lineSplit[++i];
                                part = part.Replace(",", "");
                                ffmpeg.Close();
                                return TimeSpan.Parse(part);
                            }
                        }
                    }
                }
            }
            catch
            {
                return TimeSpan.Zero; //exception was thrown, conversion failed
            }

            ffmpeg.Close();
            return TimeSpan.Zero;
        }
    }
}
