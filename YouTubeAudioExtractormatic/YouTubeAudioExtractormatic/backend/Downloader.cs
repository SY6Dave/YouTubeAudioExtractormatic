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
            if(guiForm != null)
            {
                guiForm.UpdateMsgLbl(downloadProgress + "% downloaded...");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DownloadProgressChanged;

        #endregion

        ThreadHandler threadHandler;
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
        private frmMain guiForm;

        /// <summary>
        /// Verifies that there is a directory set up for downloads
        /// </summary>
        /// <param name="threadHandler">The main program's thread manager</param>
        public Downloader(ThreadHandler threadHandler, frmMain callingForm)
        {
            this.threadHandler = threadHandler;
            this.guiForm = callingForm;

            //check ffmpeg in right place
            if(!File.Exists(ffmpegPath))
            {
                if (guiForm != null)
                {
                    guiForm.UpdateMsgLbl("ffmpeg.exe not found in lib folder!");
                }
                else
                {
                    throw new FileNotFoundException("ffmpeg.exe not found in lib folder!");
                }
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
                    if (guiForm != null)
                    {
                        guiForm.UpdateMsgLbl("Unable to create downloads directory!");
                    }
                    else
                    {
                        throw new DirectoryNotFoundException("Unable to create downloads directory!");
                    }
                }
            }
        }

        /// <summary>
        /// Invoke a thread to download a given YouTube video (or as an mp3 if provided with a bitrate)
        /// </summary>
        /// <param name="url">Video URL</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        public void BeginDownloadThread(string url, uint bitrate = 0)
        {
            if(!Directory.Exists(downloadsPath))
            {
                Directory.CreateDirectory(downloadsPath);
            }

            object[] argsArray = {url, bitrate};
            Thread downloadThread = new Thread(BeginDownload);
            threadHandler.AddActive(downloadThread);
            downloadThread.Start(argsArray);
        }

        /// <summary>
        /// Download a YouTube video based on the given url and bitrate in the object array
        /// </summary>
        /// <param name="threadArgs">An object array that must contain a string url and a uint32 bitrate</param>
        private void BeginDownload(object threadArgs)
        {
            object[] argsArray = (object[])threadArgs;
            string url;
            uint bitrate;
            url = (string)argsArray[0];
            bitrate = (uint)argsArray[1];

            using(var cli = Client.For(new YouTube())) //use a libvideo client to get video metadata
            {
                IEnumerable<YouTubeVideo> downloadLinks  = null;

                try
                {
                    downloadLinks = cli.GetAllVideos(url).OrderBy(br => -br.AudioBitrate); //sort by highest audio quality
                }
                catch(ArgumentException)
                {
                    //invalid url
                    if(guiForm != null)
                    {
                        guiForm.UpdateMsgLbl("Invalid URL!");
                    }

                    threadHandler.RemoveActive(Thread.CurrentThread);
                    Thread.CurrentThread.Abort();
                }

                var highestQuality = downloadLinks.First(); //grab best quality link

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
                        using (var bytes = new MemoryStream())
                        {
                            while (bytes.Length < len)
                            {
                                try
                                {
                                    var read = stream.Read(buffer, 0, buffer.Length);
                                    if (read > 0)
                                    {
                                        bytes.Write(buffer, 0, read);
                                        DownloadProgress = (int)(bytes.Length * 100 / len);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    //thread aborted
                                    return;
                                }
                            }

                            //check integrity of byte array
                            if (bytes.Length != len)
                            {
                                if(guiForm != null)
                                {
                                    guiForm.UpdateMsgLbl("File content is corrupted!");
                                    threadHandler.RemoveActive(Thread.CurrentThread);
                                    Thread.CurrentThread.Abort();
                                }
                                else
                                {
                                    throw new WebException("File content is corrupted.");
                                }
                            }
                            else
                            {
                                if(bitrate == 0) //video
                                {
                                    string videoPath = Path.Combine(downloadsPath, highestQuality.FullName);
                                    File.WriteAllBytes(videoPath, bytes.ToArray());
                                    if (guiForm != null)
                                    {
                                        guiForm.UpdateMsgLbl("Successful!");
                                    }
                                }
                                else //mp3
                                {
                                    //create temp video file to convert to mp3 and dispose of when done
                                    using (var tempVideo = new TempFile())
                                    {
                                        File.WriteAllBytes(tempVideo.Path, bytes.ToArray());
                                        TimeSpan duration = GetVideoDuration(tempVideo.Path);
                                        string audioPath = Path.Combine(downloadsPath, highestQuality.FullName + ".mp3");

                                        if (guiForm != null)
                                        {
                                            guiForm.UpdateMsgLbl("Converting to mp3... Do not close this window!");
                                        }

                                        ToMp3(tempVideo.Path, audioPath, duration, bitrate); //convert to mp3

                                        if (guiForm != null)
                                        {
                                            guiForm.UpdateMsgLbl("Successful!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Convert a video file to an mp3 of a desired bitrate
        /// </summary>
        /// <param name="videoPath">The file path of the video</param>
        /// <param name="audioPath">The file path to save the mp3</param>
        /// /// <param name="duration">The duration of the video file. Used to calculate progress</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        /// <returns>Returns true if the conversion was successful</returns>
        private bool ToMp3(string videoPath, string audioPath, TimeSpan duration, uint bitrate = 320)
        {
            Console.WriteLine("Converting video to mp3 at a bitrate of {0}kbit/s", bitrate);
            //setup an ffmpeg process
            var ffmpeg = new Process
            {
                StartInfo = { UseShellExecute = false, RedirectStandardError = true, FileName = ffmpegPath }
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
                    Console.WriteLine("Unable to start ffmpeg!");
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
                                Console.WriteLine((int)percentage + "%");
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred while converting\n\n{0}", e.Message);
                return false; //exception was thrown, conversion failed
            }

            ffmpeg.Close();

            Console.WriteLine("Converted!");
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
