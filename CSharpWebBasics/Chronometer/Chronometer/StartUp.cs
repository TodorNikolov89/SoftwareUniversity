using System;
using System.Linq;

namespace Chronometer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IChronometer chronometer = new Chronometer();


            while (true)
            {
                string command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "start":
                        chronometer.Start();
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "laps":
                        Console.WriteLine("Laps:\n\r" + (chronometer.Laps.Count == 0
                                                                               ? "no laps."
                                                                               : string.Join("\n\r", chronometer.Laps.Select((lap, index) => $"{index}. {lap}"))));
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
