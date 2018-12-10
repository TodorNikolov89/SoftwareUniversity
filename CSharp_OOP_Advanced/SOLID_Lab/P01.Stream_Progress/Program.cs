using System;

namespace P01.Stream_Progress
{
    public class Program
    {
        static void Main()
        {
            var progressInfo = new StreamProgressInfo(new File("Todor", 1000, 10));
            progressInfo.CalculateStreamProgress();

        }
    }
}
