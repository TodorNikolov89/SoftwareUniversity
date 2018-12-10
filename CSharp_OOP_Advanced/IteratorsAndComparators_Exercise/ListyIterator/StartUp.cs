using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Problem1_ListyIterator
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] line = Console.ReadLine().Split();

            ListyIterator<string> data;

            if (line.Length == 0)
            {
                data = new ListyIterator<string>();
            }
            else
            {
                data = new ListyIterator<string>(line.Skip(1));
            }

            Console.WriteLine(ExecuteCommands(data));

        }

        private static string ExecuteCommands(ListyIterator<string> data)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                string line = Console.ReadLine();

                while (line != "END")
                {
                    string[] info = line.Split();
                    string command = info[0];

                    switch (command)
                    {
                        case "Move":
                            sb.AppendLine(data.Move().ToString());
                            break;
                        case "Print":
                            sb.AppendLine(data.Print());
                            break;
                        case "HasNext":
                            sb.AppendLine(data.HasNext().ToString());
                            break;
                        case "PrintAll":
                            foreach (var item in data)
                            {
                                sb.Append(item + " ");
                            }
                            sb.Remove(sb.Length - 1, 1);
                            sb.AppendLine();
                            break;
                    }

                    line = Console.ReadLine();
                }
            }
            catch (ArgumentException ex)
            {
                sb.AppendLine(ex.Message);
            }

            return sb.ToString().TrimEnd();

        }
    }
}
