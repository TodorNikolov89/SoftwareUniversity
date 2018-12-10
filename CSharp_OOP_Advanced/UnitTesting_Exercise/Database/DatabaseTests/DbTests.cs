namespace DatabaseTests
{
    using Database;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class DbTests
    {
        private const int Size = 16;
        private const int InitiaArrayindex = -1;
        private Database db;

        [SetUp]
        public void SetUp()
        {
            Database database = new Database();
        }

        [Test]
        public void TestEmptyConstructorInitializeData()
        {
            //Arrange
             db = new Database();

            //Act
            var type = typeof(Database);
            var field = (int[])type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "data")
                .GetValue(db);

            var size = field.Length;

            //Assert
            Assert.That(size, Is.EqualTo(16));
        }

        [Test]
        public void TestEmptyConstructorInitializeIndexToMinusOne()
        {
            //Arrange
            db = new Database();

            //Act
            var type = typeof(Database);
            var index = (int)type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            //Assert
            Assert.That(index, Is.EqualTo(InitiaArrayindex));
        }

        [Test]
        public void TestIfCtorThrowsAnExceptionWithLargerArray()
        {
            int[] arr = new int[17];
            var type = typeof(Database);

            Assert.Throws<InvalidOperationException>(() => new Database(arr));
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 13 })]
        public void TestIfCtorSetsIndexCorrectly(int[] arr)
        {
            //Arrange
             db = new Database(arr);
            var type = typeof(Database);

            //Act
            var index = type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            int expectedIndex = arr.Length - 1;

            //Assert
            Assert.That(index, Is.EqualTo(expectedIndex));
        }

        [Test]
        public void TestIfAddMethodThrowsExceptionIfIndexIsFifteen()
        {
            //Arrange
             db = new Database(new int[16]);

            //Act
            int index = (int)typeof(Database)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            //Assert
            Assert.That(() => db.Add(1),
                Throws.InvalidOperationException.With.Message.EqualTo("Database is full"));
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 })]
        public void TestIfAddMethodAddsValueAtTheNextFreeCell(int[] arr)
        {
            //Arrange
             db = new Database(arr);
            int value = 1;

            //Act
            db.Add(value);

            int[] array = (int[])typeof(Database)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "data")
                .GetValue(db);

            int index = (int)typeof(Database)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            int actualResult = array[index];
            int expectedResult = value;

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestIfRemoveMethodThrowsAnExceptionIfIndexIsMinusOne()
        {
            //Arrange
             db = new Database(new int[0]);

            //Act
            int index = (int)typeof(Database)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            //Assert
            Assert.That(() => db.Remove(),
                Throws.InvalidOperationException.With.Message.EqualTo("Database is empty"));
        }

        [Test]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 5 })]
        [TestCase(new int[] { 1, 2 })]
        public void TestIfRemoveMethodDecreaseDataSize(int[] arr)
        {
            //Arrange
             db = new Database(arr);

            var type = typeof(Database);
            int actualIndex = (int)type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            db.Remove();

            int expectedIndex = (int)type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(x => x.Name == "index")
                .GetValue(db);

            //Assert
            Assert.AreEqual(actualIndex, expectedIndex + 1);
        }
    }
}
