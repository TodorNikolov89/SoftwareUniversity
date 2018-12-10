using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class AxeTests
    {
        private const int dummyHealth = 10;
        private const int dummyExperience = 10;

        private Dummy dummy;

        [SetUp]
        public void SetUp()
        {
            this.dummy = new Dummy(dummyHealth, dummyExperience);
        }

        [TestCase(10, 10)]
        public void TestIfWeaponLosesDurabilityAfterEachAttack(int attackingPoints, int durability)
        {
            //Arrange
            Axe axe = new Axe(attackingPoints, durability);

            //Act
            axe.Attack(dummy);

            //Assert
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe durability doens't change after attack!");
        }

        [TestCase(1, -5)]
        [TestCase(5, 0)]
        public void TestAttackingWithBrokenWeapon(int attackingPoints, int durability)
        {
            //Arrange
            Axe axe = new Axe(attackingPoints, durability);

            //Act

            //Assert
            Assert.That(() => axe.Attack(dummy),
                Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."));
        }
    }
}
