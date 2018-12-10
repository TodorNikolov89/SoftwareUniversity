using System;
using System.Collections.Generic;
using System.Linq;

namespace SortWords
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<string> words = Console.ReadLine().Split().ToList();

            words.Sort();

            Console.WriteLine(string.Join(" ", words));
        }
    }
}
