using System;
using System.Collections.Generic;
using System.Linq;

namespace SumAndAverage
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();

            int sum = 0;
            double average = 0.00;

            if(input == string.Empty)
            {
                Console.WriteLine($"Sum={sum}; Average={average:f2}");
            }
            else
            {
                List<int> nums = input
                    .Split()
                    .Select(int.Parse)
                    .ToList();

                sum = nums.Sum();
                average = nums.Average();

                Console.WriteLine($"Sum={sum}; Average={average:f2}");
            }
            
        }
    }
}
