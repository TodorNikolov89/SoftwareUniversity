using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMester.BusinessLogic.Tests
{
    [TestFixture]
    public class StorageMasterTests
    {
        private Type storageMaster;

        [SetUp]
        public void SetUp()
        {
            this.storageMaster = GetType("StorageMaster");
        }

        [Test]
        public void ValidateAddMethod()
        {
            var addProductMethod = storageMaster.GetMethod("AddProduct");

            var instance = Activator.CreateInstance(storageMaster);

            var productType = "Gpu";
            var productPrice = 12.21;

            var actualResult = addProductMethod.Invoke(instance, new object[] { productType, productPrice });
            var expectedResult = "Added Gpu to pool";

            Assert.AreEqual(expectedResult, actualResult);

            var productPoolField = (IDictionary<string, Stack<Product>>)storageMaster.GetField("productsPool", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);

            Assert.That(productPoolField[productType].Count(), Is.EqualTo(1));
        }

        [Test]
        public void ValidateRegisterStorage()
        {
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");

            var instance = Activator.CreateInstance(storageMaster);

            var storageType = "Warehouse";
            var name = "Todor";

            var expectedResult = "Registered Todor";
            var actualResult = registerStorageMethod.Invoke(instance, new object[] { storageType, name });

            Assert.AreEqual(expectedResult, actualResult);

            var storageRegistryField = (IDictionary<string, Storage>)storageMaster.GetField("storageRegistry", (BindingFlags)62).GetValue(instance);

            Assert.That(storageRegistryField[name].Name, Is.EqualTo(name));

        }

        [Test]
        public void ValidateSelectVehicle()
        {
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerstorageMethod = storageMaster.GetMethod("RegisterStorage");

            var instance = Activator.CreateInstance(storageMaster);

            var storageName = "Warehouse";
            var name = "Todor";

            registerstorageMethod.Invoke(instance, new object[] { storageName, name });
            var actualResult = selectVehicleMethod.Invoke(instance, new object[] { name, 0 });
            var expectedResult = $"Selected Semi";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ValidateLoadVehicle()
        {
            var selectVehicleMethod = storageMaster.GetMethod("SelectVehicle");
            var registerstorageMethod = storageMaster.GetMethod("RegisterStorage");
            var loadVehicleMethod = storageMaster.GetMethod("LoadVehicle");
            var addProductMethod = storageMaster.GetMethod("AddProduct");

            var instance = Activator.CreateInstance(storageMaster);

            var storageName = "Warehouse";
            var name = "Todor";

            registerstorageMethod.Invoke(instance, new object[] { storageName, name });
            selectVehicleMethod.Invoke(instance, new object[] { name, 0 });

            var productType1 = "Gpu";
            var productPrice1 = 12.21;
            var productType2 = "HardDrive";
            var productPrice2 = 12.21;

            addProductMethod.Invoke(instance, new object[] { productType1, productPrice1 });
            addProductMethod.Invoke(instance, new object[] { productType2, productPrice2 });

            string[] productsToBeLoaded = new[]
            {
                "HardDrive",
                "Gpu"
            };

            var actualResult = loadVehicleMethod.Invoke(instance, new object[] { productsToBeLoaded });
            var expectedResult = $"Loaded 2/2 products into Semi";

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [Test]
        public void ValidateSendVehicleTo()
        {
            var registerStorageMethod = storageMaster.GetMethod("RegisterStorage");
            var sendVehicleTo = storageMaster.GetMethod("SendVehicleTo");
            var instance = Activator.CreateInstance(storageMaster);

            var sourceStorageName = "Warehouse";
            var sourcePersonName = "Todor";
            var destinationStorageName = "AutomatedWarehouse";
            var destinationPersonName = "Gosho";

            registerStorageMethod.Invoke(instance, new object[] { sourceStorageName, sourcePersonName });
            registerStorageMethod.Invoke(instance, new object[] { destinationStorageName, destinationPersonName });

            var actualResult = sendVehicleTo.Invoke(instance, new object[] { sourcePersonName, 0, destinationPersonName });

            var expectedResult = $"Sent Semi to Gosho (slot 1)";

            Assert.AreEqual(expectedResult, actualResult);
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
