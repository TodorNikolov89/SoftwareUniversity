using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestSubsequence
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<int> input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int index = 0;
            int bestIndex = 0;

            int lenght = 1;
            int bestLenght = 1;

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] == input[i - 1])
                {
                    lenght++;
                }
                else
                {
                    if (lenght > bestLenght)
                    {
                        bestLenght = lenght;
                        bestIndex = index;
                    }

                    index = i;
                    lenght = 1;
                }
            }

            if(lenght > bestLenght)
            {
                bestLenght = lenght;
                bestIndex = index;
            }

            List<int> result = input.GetRange(bestIndex, bestLenght);

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
