﻿using EqualityLogic.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EqualityLogic.Models
{
    public class Person : IPerson, IComparable<Person>
    {
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }


        public string Name { get; private set; }

        public int Age { get; private set; }


        public int CompareTo(Person other)
        {
            int result = this.Name.CompareTo(other.Name);

            if (result == 0)
            {
                result = this.Age.CompareTo(other.Age);
            }

            return result;
        }
    }
}
