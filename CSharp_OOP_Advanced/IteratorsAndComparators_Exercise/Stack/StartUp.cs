using Stack.Models;
using System;
using System.Linq;

namespace Stack
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            CustomStack<int> customStack = new CustomStack<int>();

            while (line != "END")
            {
                string[] input = line
               .Split(new string[] { ", ", " " }, StringSplitOptions.RemoveEmptyEntries)
               .ToArray();

                string command = input[0];
                try
                {
                    switch (command)
                    {
                        case "Push":
                            customStack.Push(input.Skip(1).Select(int.Parse).ToList());
                            break;
                        case "Pop":
                            customStack.Pop();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
                line = Console.ReadLine();
            }

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(string.Join(Environment.NewLine, customStack));
            }
        }
    }
}
