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
                    return null; //invalid playlist id
                }
            }

            return videoDatas;
        }

        private string ValidatePlaylistID(string playlistID)
        {
            if (!playlistID.Contains("?list=")) return playlistID;

            return playlistID.Remove(0, playlistID.IndexOf("?list=") + 6);
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
