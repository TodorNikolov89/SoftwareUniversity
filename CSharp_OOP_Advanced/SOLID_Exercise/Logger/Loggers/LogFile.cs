namespace Solid.Logger.Loggers
{
    using Solid.Logger.Loggers.Contracts;
    using System.Linq;

    public class LogFile : ILogFile
    {
        public int Size { get; private set; }

        public void Write(string message)
        {
	//GetSize
            this.Size += message.Where(char.IsLetter).Sum(x => x);
        }
    }
}
