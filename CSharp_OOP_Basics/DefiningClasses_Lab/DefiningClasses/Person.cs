using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    //Add public definition
    public class Person
    {
        public Person() { }

        private int age;
        private string name;

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }

        }


        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


    }
}
