using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StorageMester.Tests.Structure
{
    [TestFixture]
    public class StorageTests
    {
        private Type storage;

        [SetUp]
        public void SetUp()
        {
            storage = GetType("Storage");
        }

        [Test]
        public void CheckIfClassIsAbstract()
        {
            Assert.That(storage.IsAbstract, $"Class is not abstract");
        }

        [Test]
        public void ValidateStorageChildClasses()
        {
            var types = new[]
            {
                "AutomatedWarehouse",
                "Warehouse",
                "DistributionCenter"
            };

            foreach (var type in types)
            {
                var currentType = GetType(type);

                Assert.That(currentType, Is.Not.Null, $"{type} doesn't exist!");
            }
        }

        [Test]
        public void ValidateConstructor()
        {
            var constructor = this.storage
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault();

            Assert.That(constructor, Is.Not.Null, $"constructor is not protected");

            var expectedCtorParams = new List<Type>
            {
                { typeof(string)},
                {typeof(int)},
                { typeof(int)},
                { typeof(IEnumerable<Vehicle>)}
            };

            var actualctorParams = constructor.GetParameters();

            for (int i = 0; i < actualctorParams.Length; i++)
            {
                Assert.That(actualctorParams[i].ParameterType, Is.EqualTo(expectedCtorParams[i]), $"Different type!");
            }
        }

        [Test]
        public void ValidateProperties()
        {
            var expectedProperties = new Dictionary<string, Type>()
            {
                { "Name", typeof(string)},
                { "Capacity", typeof(int)},
                { "GarageSlots", typeof(int)},
                { "IsFull", typeof(bool)},
                { "Garage", typeof(IReadOnlyCollection<Vehicle>)},
                { "Products", typeof(IReadOnlyCollection<Product>)}
            };

            var actualProperties = this.storage.GetProperties(BindingFlags.Public | BindingFlags.Instance);


            foreach (var property in actualProperties)
            {
                var isValidProperty = expectedProperties.Any(x => x.Key == property.Name
                && x.Value == property.PropertyType);
                Assert.That(isValidProperty, $"{property} doesn't exist!");
            }

        }

        [Test]
        public void ValidateMethods()
        {
            var actualMethods = new List<Method>()
            {
              new Method( typeof(Vehicle), "GetVehicle", typeof(int)),
              new Method( typeof(int), "SendVehicleTo", typeof(int), typeof(Storage)),
              new Method( typeof(int), "UnloadVehicle", typeof(int)),
              new Method( typeof(void), "InitializeGarage", typeof(IEnumerable<Vehicle>)),
              new Method( typeof(int), "AddVehicle", typeof(Vehicle))
            };

            foreach (var actualMethod in actualMethods)
            {
                var expectedMethod = this.storage.GetMethod(actualMethod.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                Assert.That(expectedMethod, Is.Not.Null, $"{actualMethod.Name} doesn't exist!");

                var expectedMethoType = expectedMethod.ReturnType == actualMethod.ReturnType;

                Assert.That(expectedMethoType, $"Different type!");

                var actualMethodParams = actualMethod.InputParameters;
                var expectedMethodParams = expectedMethod.GetParameters();

                for (int i = 0; i < expectedMethodParams.Length; i++)
                {
                    var actualParam = actualMethodParams[i];
                    var expectParam = expectedMethodParams[i].ParameterType;

                    Assert.AreEqual(expectParam, actualParam, $"Different type of parameter!");
                }
            }
        }

        [Test]
        public void ValidateStorageFields()
        {
            var expectedFieldGarage = storage.GetField("garage", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(expectedFieldGarage.IsStatic, Is.Not.Null, $"Field garage doesn't exist!");

            var expectedFieldProducts = storage.GetField("products", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.That(expectedFieldProducts.IsStatic, Is.Not.Null, $"Field products doesn't exist!");

        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(x => x.Name == type);

            return targetType;
        }

        private class Method
        {
            public Method(Type type, string name, params Type[] inputParameters)
            {
                this.ReturnType = type;
                this.Name = name;
                this.InputParameters = inputParameters;
            }

            public Type ReturnType { get; set; }

            public string Name { get; set; }

            public Type[] InputParameters { get; set; }
        }
    }
}
