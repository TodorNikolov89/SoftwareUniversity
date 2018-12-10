using CustomLinkedList;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace CustomLInkedListUnitTests
{
    [TestFixture]
    public class DynamicListTests
    {
        private DynamicList<int> dynamicList;

        [SetUp]
        public void SetUp()
        {
            this.dynamicList = new DynamicList<int>();
        }

        [Test]
        public void TestItemPropertyWithNegativeIndex()
        {
            //Arrange
            int incorrectIndex = -1;

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var test = this.dynamicList[incorrectIndex];
            },
            $"Invalid index {incorrectIndex}");
        }

        [Test]
        public void TestItemPropertyWithIndexGreaterThanRange()
        {
            //Arrange
            int incorrectIndex = 5;

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var test = this.dynamicList[incorrectIndex];
            },
            $"The index is greater than the range of collection");
        }

        [Test]
        public void AddShouldIncreaseTheCollection()
        {
            this.dynamicList.Add(5);

            Assert.AreEqual(1, this.dynamicList.Count, "Adding element doens't incease the count!");
        }

        [Test]
        public void RemoveShouldDecreaseTheCollection()
        {
            //Arrange
            this.dynamicList.Add(1);
            this.dynamicList.Add(2);
            this.dynamicList.Add(3);

            this.dynamicList.Remove(1);
            
            //Assert
            Assert.AreEqual(2, this.dynamicList.Count, "Removing element doesn't decrease collection!");
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(4, 5)]
        [TestCase(6, 7)]
        public void RemoveUnexistantElementShoulReturnNegativeNumber(int inputNumber, int removeNumber)
        {
            //Arrange
            this.dynamicList.Add(inputNumber);

            int expectedNumber = -1;

            //Act
            int actualNumber = this.dynamicList.Remove(removeNumber);

            //Assert
            Assert.That(expectedNumber, Is.EqualTo(actualNumber), $"Element doesn't exist!");
        }

        [Test]
        public void RemoveAtIndexShouldRemoveElementAtSpecifiedIndex()
        {
            //Arrange
            this.dynamicList.Add(1);
            this.dynamicList.Add(2);
            this.dynamicList.Add(3);

            var expectedResult = 2;
            
            //Act
            var actualResult = this.dynamicList.RemoveAt(1);

            //Assert
            Assert.AreEqual(expectedResult, actualResult, $"The returned element is not the same as expected!");
        }

        [TestCase(3)]
        [TestCase(6)]
        [TestCase(4)]
        public void IndexOfShouldReturnNegativeValueIfNumberDosnotExist(int findNumber)
        {
            //Arrange
            this.dynamicList.Add(1);
            this.dynamicList.Add(2);
            this.dynamicList.Add(5);

            var expectedResult = -1;

            //Act
            var actualResult = this.dynamicList.IndexOf(findNumber);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(5, 2)]
        public void IndexOfShouldReturnIndexOfTheNumber(int findNumber, int result)
        {
            //Arrange
            this.dynamicList.Add(1);
            this.dynamicList.Add(2);
            this.dynamicList.Add(5);

            var expectedResult = result;

            //Act
            var actualResult = this.dynamicList.IndexOf(findNumber);


            //Assert
            Assert.AreEqual(expectedResult, actualResult, $"Number {findNumber} doesn't exist!");
        }


        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(7, true)]
        public void ContainsShouldReturnFalseIfElementDoesNotExists(int findNumber, bool isFound)
        {
            //Arrange
            this.dynamicList.Add(55);
            this.dynamicList.Add(5);
            this.dynamicList.Add(4);

            var expectedResult = isFound;

            //Act
            var actualResult = this.dynamicList.Contains(findNumber);

            //Assert
            Assert.IsFalse(actualResult, $"Contains methid returns true for existing element!");
        }

        [TestCase(55, true)]
        [TestCase(5, true)]
        [TestCase(4, true)]
        public void ContainsShouldReturnTrueIfElementExists(int findNumber, bool isFound)
        {
            //Arrange
            this.dynamicList.Add(55);
            this.dynamicList.Add(5);
            this.dynamicList.Add(4);

            var expectedResult = isFound;

            //Act
            var actualResult = this.dynamicList.Contains(findNumber);

            //Assert
            Assert.IsTrue(actualResult, $"Contains methid returns false for existing element!");
        }

    }
}
