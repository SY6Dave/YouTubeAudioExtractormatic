using System;
using System.IO;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Provides functionality to create a temp file which is disposed of when out of scope or destructed
    /// </summary>
    public class TempFile : IDisposable
    {
        string path;

        /// <summary>
        /// Construct a temp file with a random name
        /// </summary>
        public TempFile() : this(System.IO.Path.GetTempFileName()) { }

        /// <summary>
        /// Construct a temp file with a given name
        /// </summary>
        /// <param name="path"></param>
        public TempFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            this.path = path;
        }

        /// <summary>
        /// Get the file path assigned to the temp file
        /// </summary>
        public string Path
        {
            get
            {
                if (path == null) throw new ObjectDisposedException(GetType().Name);
                return path;
            }
        }

        /// <summary>
        /// Dispose when destructing
        /// </summary>
        ~TempFile() { Dispose(false); }
        public void Dispose() { Dispose(true); }
        private void Dispose(bool disposing)
        {
            if(disposing)
            {
                GC.SuppressFinalize(this);
            }

            //delete file
            if(path != null)
            {
                try { File.Delete(path); }
                catch { }
                path = null;
            }
        }
    }
}
