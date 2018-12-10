using P01.Stream_Progress.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private IStreamable streamProgress;

        public StreamProgressInfo(IStreamable streamResult)
        {
            this.streamProgress = streamResult;
        }

        public int CalculateStreamProgress()
        {
            return (this.streamProgress.BytesSent * 100) / this.streamProgress.Length;
        }

    }
}
