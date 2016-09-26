using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using System.Diagnostics;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Provides functionality to interface with the Google YouTube v3 api
    /// </summary>
    public class YoutubeGrabber
    {
        private static string apikey = "AIzaSyAApYbIaWadnBvNbpWHn_QrfqZRx7mGMqM";
        YouTubeService youtubeService;

        /// <summary>
        /// Initialise the YouTube service with apikey
        /// </summary>
        public YoutubeGrabber()
        {
            var ys = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apikey,
                ApplicationName = this.GetType().ToString()
            });

            this.youtubeService = ys;

            if (!TestConnection()) throw new ArgumentException("api key invalid");
        }

        public List<VideoData> GetVideosByPlaylist(string playlistID)
        {
            playlistID = UrlParser.GetFormattedUrl(playlistID);
            playlistID = ValidatePlaylistID(playlistID);

            List<VideoData> videoDatas = new List<VideoData>();

            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var request = youtubeService.PlaylistItems.List("snippet,contentDetails");
                request.PlaylistId = playlistID;
                request.MaxResults = 50;
                request.PageToken = nextPageToken;

                try
                {
                    var response = request.Execute();
                    foreach(var video in response.Items)
                    {
                        VideoData currVideo = new VideoData(video.ContentDetails.VideoId, video.Snippet.Title);
                        videoDatas.Add(currVideo);
                    }

                    nextPageToken = response.NextPageToken;
                }
                catch
                {
                    VideoData video = GetVideo(playlistID);
                    if (video == null) return null; //not a valid playlist or video

                    return new List<VideoData>() { video };
                }
            }

            return videoDatas;
        }

        private VideoData GetVideo(string videoID)
        {
            videoID = ValidateVideoID(videoID);
            var request = youtubeService.Videos.List("snippet,contentDetails");
            request.Id = videoID;

            try
            {
                var response = request.Execute();
                return new VideoData(response.Items[0].Id, response.Items[0].Snippet.Title);
            }
            catch
            {
                return null; //invalid video id
            }
        }

        private string ValidateVideoID(string videoID)
        {
            if (!videoID.Contains("watch?v=")) return videoID;

            string id = videoID.Remove(0, videoID.IndexOf("watch?v=") + 8);
            if (id.Contains("&"))
            {
                int index = id.IndexOf("&");
                id = id.Remove(index, id.Length - index);
            }
            return id;
        }

        private string ValidatePlaylistID(string playlistID)
        {
            if (!playlistID.Contains("list=")) return playlistID;

            string id = playlistID.Remove(0, playlistID.IndexOf("list=") + 5);
            if(id.Contains("&"))
            {
                int index = id.IndexOf("&");
                id = id.Remove(index, id.Length - index);
            }
            return id;
        }

        private bool TestConnection()
        {
            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = "cat";
            searchRequest.MaxResults = 5;

            try
            {
                var searchResponse = searchRequest.Execute();
                foreach (var searchItem in searchResponse.Items)
                {
                    if (searchItem.Id.VideoId != null)
                    {
                        Debug.WriteLine(searchItem.Snippet.Title);
                    }
                }

                return true;
            }
            catch
            {
                //something went wrong
                return false;
            }
        }
    }
}
