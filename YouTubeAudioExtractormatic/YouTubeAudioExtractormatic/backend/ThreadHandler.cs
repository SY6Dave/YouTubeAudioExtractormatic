using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YouTubeAudioExtractormatic
{
    /// <summary>
    /// Allows download threads to be kept track of and aborted if necessary
    /// </summary>
    public class ThreadHandler
    {
        private List<Thread> activeThreads;
        public List<Thread> ActiveThreads { get { return activeThreads; } }

        /// <summary>
        /// Initialise active threads list
        /// </summary>
        public ThreadHandler()
        {
            this.activeThreads = new List<Thread>();
        }

        /// <summary>
        /// Add a thread to the active list
        /// </summary>
        /// <param name="t">The thread which is being started</param>
        public void AddActive(Thread t)
        {
            activeThreads.Add(t);
        }

        /// <summary>
        /// Remove a thread from the active list
        /// </summary>
        /// <param name="t">The thread which is being stopped</param>
        public void RemoveActive(Thread t)
        {
            if (activeThreads.Contains(t)) activeThreads.Remove(t);
        }

        public void RemoveActive(int id)
        {
            for (int i = 0; i < activeThreads.Count; i++)
            {
                var thread = activeThreads[i];
                if (thread.ManagedThreadId == id)
                {
                    RemoveActive(thread);
                    break;
                }
            }
        }

        /// <summary>
        /// Iterate through all active threads and stop them individually
        /// </summary>
        public void AbortAllThreads()
        {
            foreach (Thread t in activeThreads)
            {
                t.Abort();
            }

            Debug.WriteLine("All threads aborted. Verifying...");
            Thread.Sleep(100); //sleep to make sure they've all returned, and check if any are still active
            bool allAborted = true;

            foreach (Thread t in activeThreads)
            {
                if (t.IsAlive)
                {
                    allAborted = false;
                    break;
                }
            }

            Debug.WriteLine("All aborted status: " + allAborted);
        }
    }
}
