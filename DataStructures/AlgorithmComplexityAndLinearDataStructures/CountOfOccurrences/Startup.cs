using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountOfOccurrences
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
               .Split()
               .Select(int.Parse)
               .ToList();

            SortedDictionary<int, int> result = new SortedDictionary<int, int>();

            foreach (var num in numbers)
            {
                if (!result.ContainsKey(num))
                {
                    result.Add(num, 1);
                }
                else
                {
                    result[num]++;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var numPair in result)
            {
                sb.AppendLine($"{numPair.Key} -> {numPair.Value} times");
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
