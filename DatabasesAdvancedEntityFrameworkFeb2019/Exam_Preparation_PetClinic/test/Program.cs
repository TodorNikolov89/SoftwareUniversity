using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>() { 1, 2, 3, 4, 5, 6, 1, 1, 1, 1 };

            var a = nums.Distinct().Count();

            nums.Distinct();

            ;
        }
    }
}
