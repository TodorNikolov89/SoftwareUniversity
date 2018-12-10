using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoveOddOccurences
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            Dictionary<int, int> result = new Dictionary<int, int>();

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
            
            Console.WriteLine(string.Join(" ", numbers.Where(n => result[n] % 2 == 0)));
        }
    }
}
