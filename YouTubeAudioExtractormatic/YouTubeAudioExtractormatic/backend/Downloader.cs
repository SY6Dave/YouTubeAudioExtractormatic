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
            gui.DisplayMessage(downloadProgress + "% downloaded...");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DownloadProgressChanged;

        #endregion

        ThreadHandler threadHandler;
        static string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
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

        private iGui gui;
        private DownloadManager downloadManager;

        /// <summary>
        /// Verifies that there is a directory set up for downloads
        /// </summary>
        /// <param name="threadHandler">The main program's thread manager</param>
        public Downloader(ThreadHandler threadHandler, iGui callingForm, DownloadManager downloadManager)
        {
            this.threadHandler = threadHandler;
            this.gui = callingForm;
            this.downloadManager = downloadManager;
            
            
            //check ffmpeg in right place
            if (!File.Exists(ffmpegPath))
            {
                gui.DisplayMessage("ffmpeg.exe not found in lib folder!");
            }
        }

        public void Download(Download video)
        {
            using (var cli = Client.For(new YouTube())) //use a libvideo client to get video metadata
            {
                IEnumerable<YouTubeVideo> downloadLinks = null;

                try
                {
                    downloadLinks = cli.GetAllVideos(video.VideoData.Url).OrderBy(br => -br.AudioBitrate); //sort by highest audio quality
                }
                catch (ArgumentException)
                {
                    video.DownloadFailed = true;
                    downloadManager.RemoveActive(video);
                    //invalid url
                    gui.DisplayMessage("Invalid URL!");

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
                    downloadManager.RemoveActive(video);
                    gui.DisplayMessage("Unable to download video");
                    gui.OnProgressChanged();
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

                                        double percentage = bytes.Length * 100 / len;
                                        if (video.DownloadProgress != percentage)
                                        {
                                            video.SetDownloadProgress(percentage);
                                            gui.OnProgressChanged();
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    video.DownloadFailed = true;
                                    downloadManager.RemoveActive(video);
                                    //thread aborted
                                    return;
                                }
                            }

                            //check integrity of byte array
                            if (bytes.Length != len)
                            {
                                video.DownloadFailed = true;
                                downloadManager.RemoveActive(video);
                                gui.DisplayMessage("File content is corrupted!");
                                threadHandler.RemoveActive(Thread.CurrentThread);
                                Thread.CurrentThread.Abort();
                            }
                            else
                            {
                                if (video.Bitrate == 0) //video
                                {
                                    video.SetConvertProgress(100);
                                    string videoPath = Path.Combine(downloadManager.DownloadsPath, highestQuality.FullName);
                                    File.Copy(tempbytes.Path, videoPath, true);

                                    gui.DisplayMessage("Successful!");
                                }
                                else //mp3
                                {
                                    //create temp video file to convert to mp3 and dispose of when done
                                    TimeSpan duration = GetVideoDuration(tempbytes.Path);
                                    string audioPath = Path.Combine(downloadManager.DownloadsPath, highestQuality.FullName + ".mp3");

                                    ToMp3(tempbytes.Path, audioPath, duration, video, video.Bitrate); //convert to mp3
                                }
                            }

                            bytes.Dispose();
                            bytes.Close();
                        }
                    }
                }
            }

            downloadManager.RemoveActive(video);
        }

        /// <summary>
        /// Convert a video file to an mp3 of a desired bitrate
        /// </summary>
        /// <param name="videoPath">The file path of the video</param>
        /// <param name="audioPath">The file path to save the mp3</param>
        /// /// <param name="duration">The duration of the video file. Used to calculate progress</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        /// <returns>Returns true if the conversion was successful</returns>
        private bool ToMp3(string videoPath, string audioPath, TimeSpan duration, Download video, uint bitrate = 320)
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
                    video.DownloadFailed = true;
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

                                if (video.ConvertProgress != percentage)
                                {
                                    video.SetConvertProgress(percentage);
                                    gui.OnProgressChanged();
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                video.DownloadFailed = true;
                downloadManager.RemoveActive(video);
                return false; //exception was thrown, conversion failed
            }

            video.SetConvertProgress(100);
            gui.OnProgressChanged();

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
