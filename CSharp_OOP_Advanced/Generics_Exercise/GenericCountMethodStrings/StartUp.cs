using System;
using System.Collections.Generic;

namespace GenericCountMethodStrings
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<string> messages = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string message = Console.ReadLine();
                messages.Add(message);
            }

            string element = Console.ReadLine();

            Box<string> box = new Box<string>(messages);
            int result = box.GetGreaterthan(element);
            Console.WriteLine(result);
        }
    }
}
