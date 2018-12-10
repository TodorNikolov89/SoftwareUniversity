using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StorageMester.Tests.Structure
{
    [TestFixture]
    public class ProductTests
    {
        private Type product;

        [SetUp]
        public void SetUp()
        {
            this.product = GetType("Product");
        }

        [Test]
        public void CheckIfClassIsAbstract()
        {
            Assert.That(product.IsAbstract, $"Class Product is not abstract!");
        }

        [Test]
        public void ValidateChildClasses()
        {
            var expectedClasses = new[]
            {
                "Gpu",
                "HardDrive",
                "Ram",
                "SolidStateDrive"
            };

            foreach (var expectClass in expectedClasses)
            {
                var actualClass = GetType(expectClass);

                Assert.That(actualClass, Is.Not.Null, $"{expectClass} doesn't exist");
            }

        }

        [Test]
        public void ValidateConstructor()
        {
            var constructor = product
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault();

            Assert.That(constructor, Is.Not.Null, $"Constructor is not protected");

            var expectedParams = new List<Type>()
            {
                typeof(double),
                typeof(double)
            };

            var actualParams = constructor.GetParameters();

            for (int i = 0; i < actualParams.Length; i++)
            {
                var expectedParam = expectedParams[i];
                var actualParam = actualParams[i].ParameterType;

                Assert.AreEqual(expectedParam, actualParam, $"Different type of parameters");
            }

        }

        [Test]
        public void ValidateProperties()
        {
            var expectedProperties = new Dictionary<string, Type>()
            {
                { "Price", typeof(double)},
                { "Weight", typeof(double)}
            };

            var actualProperties = product.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in actualProperties)
            {
                var isValidProperty = expectedProperties.Any(x => x.Key == property.Name
                 && x.Value == property.PropertyType);

                if (property.Name == "Price")
                {
                    Assert.That(property.SetMethod.IsPrivate, $"The Setter method for {property.Name} is not private");
                }

                Assert.That(isValidProperty, $"Property {property.Name} doesn't exist!");
            }



        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == type);

            return targetType;
        }

    }
}
