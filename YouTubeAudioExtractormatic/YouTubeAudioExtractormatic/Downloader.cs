using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace YouTubeAudioExtractormatic
{
    public class Downloader
    {
        static string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
        string downloadsPath = Path.Combine(applicationPath, "Downloads");
        string ffmpegPath = Path.Combine(applicationPath, "lib\\ffmpeg.exe");

        public Downloader()
        {

        }

        public void BeginDownload(string url)
        {
            using(var cli = Client.For(new YouTube()))
            {
                var downloadLinks = cli.GetAllVideos(url).OrderBy(br => -br.AudioBitrate);
                var highestQuality = downloadLinks.First();
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
                                var read = stream.Read(buffer, 0, buffer.Length);
                                if (read > 0)
                                {
                                    bytes.Write(buffer, 0, read);
                                    int downloadProgress = (int)(bytes.Length * 100 / len); //use this later
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (bytes.Length != len)
                            {
                                throw new WebException("File content is corrupted.");
                            }
                            else
                            {
                                File.WriteAllBytes(videoPath, bytes.ToArray());
                            }
                        }
                    }
                }

                string audioPath = Path.Combine(downloadsPath, highestQuality.FullName + ".mp3");
                ToMp3(videoPath, audioPath);
                File.Delete(videoPath);
            }
        }

        private bool ToMp3(string videoPath, string audioPath, uint bitrate = 320)
        {
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
                if (!ffmpeg.Start())
                {
                    return false;
                }
                var reader = ffmpeg.StandardError;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            catch
            {
                return false;
            }

            ffmpeg.Close();
            return true;
        }
    }
}
