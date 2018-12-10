namespace ExtendedDatabaseTests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class ExtendedDbTests
    {
        private List<IPerson> persons;
        private Database db;

        [SetUp]
        public void SetUp()
        {
            this.persons = new List<IPerson>()
            {
                new Person("Pesho", 1241),
                new Person("Gosho", 532532),
                new Person("Misho", 53454353),
                new Person("Svetlin", 212324112)
            };

            this.db = new Database();
        }

        [Test]
        public void TestIfAddMethodThrowsAnExceptionForUserWithSameUsername()
        {
            IPerson testPerson = new Person("Pesho", 123456);
            //Arrange
            db = new Database(persons);

            //Assert
            Assert.That(() => db.Add(testPerson),
                Throws.InvalidOperationException.With.Message.EqualTo("Person with that name already exist!"));
        }

        [Test]
        public void TestIfAddMethodThrowsAnExceptionForUserWithSameId()
        {
            IPerson testPerson = new Person("Stamo", 532532);

            //Arrange
            db = new Database(persons);

            //Assert
            Assert.That(() => db.Add(testPerson),
                Throws.InvalidOperationException.With.Message.EqualTo("Person with that id already exist!"));
        }

        [Test]
        public void TestIfRemoveMethodThrowsAnExceptionForEmptyDatabase()
        {
            IPerson testPerson = new Person("Stamo", 214112);

            Assert.That(() => db.Remove(testPerson),
                Throws.InvalidOperationException.With.Message.EqualTo("The database is empty!"));
        }

        [Test]
        public void TestFindByUsernameMethodWithUsernamaEqualToNull()
        {
            string username = null;

            Assert.That(() => db.FindByUsername(username),
                Throws.ArgumentNullException);
        }

        [Test]
        public void TestIfFindByUsernameThrowsAnExceptionWithWrongUsernama()
        {
            db = new Database(persons);

            string username = "Peshoo";
            Assert.Throws<InvalidOperationException>(() => db.FindByUsername(username), "There is no person with that username");
        }

        [Test]
        public void TestIfFindByIdThrowsAnExceptionForNegativeId()
        {
            db = new Database(persons);

            long id = -1241;

            Assert.Throws<ArgumentNullException>(() => db.FindById(id));
        }

        [Test]
        public void TestIfFindByIdThrowsAnExceptionforMissingPerson()
        {
            db = new Database(persons);

            long id = 657865;

            Assert.Throws<InvalidOperationException>(() => db.FindById(id));
        }
    }
}
