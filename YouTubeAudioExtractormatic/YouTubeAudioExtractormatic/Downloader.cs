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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DownloadProgressChanged;

        #endregion

        ThreadHandler threadHandler;
        static string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
        string downloadsPath = Path.Combine(applicationPath, "Downloads");
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

        /// <summary>
        /// Verifies that there is a directory set up for downloads
        /// </summary>
        /// <param name="threadHandler">The main program's thread manager</param>
        public Downloader(ThreadHandler threadHandler)
        {
            this.threadHandler = threadHandler;

            //check ffmpeg in right place
            if(!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("ffmpeg.exe not found in lib folder!");
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
                    throw new DirectoryNotFoundException("unable to create downloads directory!");
                }
            }
        }

        /// <summary>
        /// Invoke a thread to download a given YouTube video as an mp3 of a chosen bitrate (default: 320kbps)
        /// </summary>
        /// <param name="url">Video URL</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        public void BeginDownloadThread(string url, uint bitrate = 320)
        {
            object[] argsArray = {url, bitrate};
            Thread downloadThread = new Thread(BeginDownload);
            threadHandler.AddActive(downloadThread);
            downloadThread.Start(argsArray);
            Debug.WriteLine("done");
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
                var downloadLinks = cli.GetAllVideos(url).OrderBy(br => -br.AudioBitrate); //sort by highest audio quality
                var highestQuality = downloadLinks.First(); //grab best quality link
                string videoPath = Path.Combine(downloadsPath, highestQuality.FullName);

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
                                throw new WebException("File content is corrupted.");
                            }
                            else
                            {
                                File.WriteAllBytes(videoPath, bytes.ToArray()); //save temp video
                            }
                        }
                    }
                }

                string audioPath = Path.Combine(downloadsPath, highestQuality.FullName + ".mp3");
                ToMp3(videoPath, audioPath, bitrate); //convert to mp3
                File.Delete(videoPath); //delete the temp video
            }
        }

        /// <summary>
        /// Convert a video file to an mp3 of a desired bitrate
        /// </summary>
        /// <param name="videoPath">The file path of the video</param>
        /// <param name="audioPath">The file path to save the mp3</param>
        /// <param name="bitrate">Determines quality of the output mp3. 320kbps = high</param>
        /// <returns>Returns true if the conversion was successful</returns>
        private bool ToMp3(string videoPath, string audioPath, uint bitrate = 320)
        {
            //setup an ffmpeg process
            var ffmpeg = new Process
            {
                StartInfo = { UseShellExecute = false, RedirectStandardError = true, FileName = ffmpegPath }
            };

            var arguments =
                String.Format(
                    @"-i ""{0}"" -b:a {1}K -vn ""{2}""",
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
                    return false;
                }
                var reader = ffmpeg.StandardError;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line); //write out any messages
                }
            }
            catch
            {
                return false; //exception was thrown, conversion failed
            }

            ffmpeg.Close();
            return true;
        }
    }
}
