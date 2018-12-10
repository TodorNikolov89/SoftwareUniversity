using ComparingObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComparingObjects
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            string input = Console.ReadLine();
            
            while (input != "END")
            {
                string[] info = input.Split();

                string name = info[0];
                int age = int.Parse(info[1]);
                string town = info[2];

                Person person = new Person(name, age, town);
                persons.Add(person);

                input = Console.ReadLine();
            }

            int index = int.Parse(Console.ReadLine()) - 1;

            Person currentPerson = persons[index];

            if (persons.Where(x => x.CompareTo(currentPerson) == 0).Count() > 1)
            {
                int equalPersons = persons.Where(x => x.CompareTo(currentPerson) == 0).Count();
                int diffPersons = persons.Where(x => x.CompareTo(currentPerson) > 0).Count();

                Console.WriteLine($"{equalPersons} {diffPersons} {persons.Count}");
            }
            else
            {
                Console.WriteLine("No matches");
            }

        }
    }
}
