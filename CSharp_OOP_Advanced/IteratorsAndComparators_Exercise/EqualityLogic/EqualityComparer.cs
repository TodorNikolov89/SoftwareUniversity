using EqualityLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EqualityLogic
{
    public class EqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            return x.CompareTo(y) == 0;
        }

        public int GetHashCode(Person person)
        {
            return $"{person.Name} {person.Age}".GetHashCode();
        }
    }
}
