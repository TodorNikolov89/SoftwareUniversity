using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void TestIfDummyLosesHealthIfAttacked()
        {
            //Arrange
            Dummy dummy = new Dummy(10, 10);
            int attackPoints = 5;

            //Act
            dummy.TakeAttack(attackPoints);
            int expectedHealth = 5;
            int actualHealth = dummy.Health;

            //Assert
            // Assert.That(dummy.Health, Is.EqualTo(5));
            Assert.AreEqual(expectedHealth, actualHealth,
                "Dummy doesn't lose health if attacked");
        }

        [Test]
        public void TestIfDeadDummyThrowsAnExceptionIfAttacked()
        {
            //Arrage
            Dummy dummy = new Dummy(0, 10);
            int attackPoints = 20;

            //Act
            // dummy.TakeAttack(attackPoints);

            //Assert
            //First way to do the Assert is:
            //Assert.That(() => dummy.TakeAttack(attackPoints),
            //   Throws.InvalidOperationException.With.Message.EqualTo("Dummy is dead."));

            //Second way to do the Assert is:
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(attackPoints));

            //Thirth way to do the Assert is:
            //Assert.Throws(typeof(InvalidOperationException), () =>dummy.TakeAttack(attackPoints));
        }

        [Test]
        public void TestIfDeadDummyCanGiveXP()
        {
            //Arrange
            Dummy dummy = new Dummy(0, 12);

            //Act
            int actualExperience = dummy.GiveExperience();
            int expectedExperience = 12;

            //Assert
            //Assert.That(actualExperience, Is.EqualTo(expectedExperience));

            Assert.AreEqual(actualExperience, expectedExperience,
                "Dummy cannot give XP");
        }

        [Test]
        public void TestIfAliveDummyCantGiveXP()
        {
            //Arrange
            Dummy dummy = new Dummy(30, 50);

            //Act

            //Assert
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }
    }
}
