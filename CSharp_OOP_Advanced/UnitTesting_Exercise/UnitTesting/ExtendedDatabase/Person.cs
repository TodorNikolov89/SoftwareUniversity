namespace ExtendedDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Person : IPerson
    {
        public Person(string name, long id)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Name { get; set; }

        public long Id { get; set; }
    }
}
