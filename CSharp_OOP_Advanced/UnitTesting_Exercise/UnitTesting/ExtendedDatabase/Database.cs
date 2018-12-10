namespace ExtendedDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class Database
    {
        private List<IPerson> people;

        public Database()
        {
            this.people = new List<IPerson>();
        }

        public Database(List<IPerson> persons)
        {
            this.people = new List<IPerson>(persons);
        }

        public void Add(IPerson person)
        {
            IPerson currentPersonName = this.people.Where(n => n.Name == person.Name).FirstOrDefault();
            if (currentPersonName != null)
            {
                throw new InvalidOperationException("Person with that name already exist!");
            }

            IPerson currentPersonId = this.people.Where(n => n.Id == person.Id).FirstOrDefault();
            if (currentPersonId != null)
            {
                throw new InvalidOperationException("Person with that id already exist!");
            }

            this.people.Add(person);
        }

        public void Remove(IPerson person)
        {
            if (!this.people.Contains(person))
            {
                throw new InvalidOperationException("The database is empty!");
            }

            this.people.Remove(person);
        }

        public IPerson FindByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }

            IPerson person = this.people
                .Where(x => x.Name == username)
                .FirstOrDefault();

            if (person == null)
            {
                throw new InvalidOperationException("There is no person with that username");
            }

            return person;
        }

        public IPerson FindById(long id)
        {
            if (id < 0)
            {
                throw new ArgumentNullException("Id cannot be negative!");
            }

            IPerson person = this.people
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (person == null)
            {
                throw new InvalidOperationException("There is no person with that id");
            }

            return person;
        }
    }
}
