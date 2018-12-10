using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMester.Tests.Structure
{
    [TestFixture]
    public class VehicleTests
    {
        private Type vehicle;

        [SetUp]
        public void SetUp()
        {
            this.vehicle = GetType("Vehicle");
        }

        [Test]
        public void CheckIfClassIsAbstract()
        {
            Assert.That(vehicle.IsAbstract, "The class is not Abstract");
        }

        [Test]
        public void ChechForChildClasses()
        {
            // Create an array with child class names
            var types = new string[]
            {
                "Semi",
                "Truck",
                "Van"
            };

            foreach (var type in types)
            {
                var currentType = GetType(type);

                Assert.That(currentType, Is.Not.Null, $"Child class {type} doesn't not exist.");
            }
        }

        [Test]
        public void TestIfConstructorIsProtected()
        {
            var ctor = this.vehicle
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault();

            Assert.That(ctor, Is.Not.Null, "Constructor doesn't exist!");

            var ctorParams = ctor.GetParameters();

            Assert.That(ctorParams[0].ParameterType,
                Is.EqualTo(typeof(int)), $"Parameter is different type!");
        }

        [Test]
        public void ValidateVehicleProperties()
        {
            //Properties from Cehicle class
            var actualProperties = typeof(Vehicle)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var expectecProperties = new Dictionary<string, Type>
            {
                { "Capacity", typeof(int)},
                { "Trunk", typeof(IReadOnlyCollection<Product>)},
                { "IsFull", typeof(bool)},
                { "IsEmpty", typeof(bool)}
            };

            foreach (var actualProperty in actualProperties)
            {
                var isValidProperty = expectecProperties.Any(x => x.Key == actualProperty.Name
                && x.Value == actualProperty.PropertyType);

                Assert.That(isValidProperty, $"{actualProperty.Name} doesn't exist!");
            }
        }

        [Test]
        public void ValidateVehicleMethods()
        {
            var actualMethods = new List<Method>
            {
            new Method(typeof(void), "LoadProduct", typeof(Product)),
            new Method(typeof(Product), "Unload")
            };

            foreach (var actualMethod in actualMethods)
            {
                var expectedMethod = vehicle.GetMethod(actualMethod.Name);

                Assert.That(expectedMethod, Is.Not.Null, $"{actualMethod.Name} doesn't exist!");

                var expectedMethodType = expectedMethod.ReturnType == actualMethod.ReturnType;

                Assert.That(expectedMethodType, Is.Not.Null, $"Invalid return type!");

                var expectedMethodParams = expectedMethod.GetParameters();
                var actualMethodParams = actualMethod.InputParameters;

                for (int i = 0; i < expectedMethodParams.Length; i++)
                {
                    var actualParam = actualMethodParams[i];
                    var expectParam = expectedMethodParams[i].ParameterType;

                    Assert.AreEqual(expectParam, actualParam);
                }
            }
        }

        [Test]
        public void ValidateVehicleFields()
        {
            var truckField = vehicle.GetField("trunk", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.That(truckField, Is.Not.Null,"Field doesn't exist");
        }

        private class Method
        {
            public Method(Type type, string name, params Type[] inputParams)
            {
                this.ReturnType = type;
                this.Name = name;
                this.InputParameters = inputParams;
            }

            public Type ReturnType { get; set; }

            public string Name { get; set; }

            public Type[] InputParameters { get; set; }
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