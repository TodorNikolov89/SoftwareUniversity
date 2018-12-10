using StrategyPattern.Models;
using StrategyPattern.Models.Comparators;
using System;
using System.Collections.Generic;

namespace StrategyPattern
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SortedSet<Person> nameSortedSet = new SortedSet<Person>(new NameComparator());
            SortedSet<Person> ageSortedSet = new SortedSet<Person>(new AgeComparator());

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] info = Console.ReadLine().Split();

                string name = info[0];
                int age = int.Parse(info[1]);

                Person person = new Person(name, age);
                nameSortedSet.Add(person);
                ageSortedSet.Add(person);
            }

            Console.WriteLine(string.Join(Environment.NewLine, nameSortedSet));
            Console.WriteLine(string.Join(Environment.NewLine, ageSortedSet));

        }
    }
}
