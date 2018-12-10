using StorageMaster.Entity.Factories;
using StorageMaster.Entity.Products;
using StorageMaster.Entity.Storages;
using StorageMaster.Entity.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Core

{
    public class StorageMaster
    {
        private ProductFactory productFactory;
        private StorageFactory storageFactory;
        private Dictionary<string, Stack<Product>> products;
        private Dictionary<string, Storage> storages;
        private Vehicle currentVehicle;

        public StorageMaster()
        {
            this.products = new Dictionary<string, Stack<Product>>();
            this.storages = new Dictionary<string, Storage>();
            this.productFactory = new ProductFactory();
            this.storageFactory = new StorageFactory();
        }

        public string AddProduct(string type, double price)
        {
            Product product = this.productFactory.CreateProduct(type, price);

            if (!this.products.ContainsKey(type))
            {
                this.products.Add(type, new Stack<Product>());
            }

            this.products[type].Push(product);

            string result = $"Added {type} to pool";
            return result;
        }

        public string RegisterStorage(string type, string name)
        {
            Storage storage = this.storageFactory.CreateStorage(type, name);

            this.storages.Add(name, storage);

            string result = $"Registered {name}";
            return result;
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            Storage storage = this.storages[storageName];

            this.currentVehicle = storage.GetVehicle(garageSlot);

            string result = $"Selected {this.currentVehicle.GetType().Name}";
            return result;

        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int counter = 0;
            foreach (var prod in productNames)
            {
                if (this.currentVehicle.IsFull)
                {
                    break;
                }

                if (!this.products.ContainsKey(prod) || this.products[prod].Count == 0)
                {
                    throw new InvalidOperationException($"{prod} is out of stock!");
                }

                Product product = this.products[prod].Pop();
                
                this.currentVehicle.LoadProduct(product);
                counter++;
            }

            string result = $"Loaded {counter}/{productNames.Count()} products into {this.currentVehicle.GetType().Name}";
            return result;
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            if (!this.storages.ContainsKey(sourceName))
            {
                throw new InvalidOperationException("Invalid source storage!");
            }

            if (!this.storages.ContainsKey(destinationName))
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            Storage sourceStorage = this.storages[sourceName];
            Storage destinationStorage = this.storages[destinationName];
            Vehicle vehicle = sourceStorage.GetVehicle(sourceGarageSlot);

           // sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            int destinationGarageSlot = sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);
            string result = $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})";
            return result;
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            Storage storage = this.storages[storageName];
            int countProductsInVehicle = storage.GetVehicle(garageSlot).Trunk.Count;
            int unloadedProductsCounter = storage.UnloadVehicle(garageSlot);

            string result = $"Unloaded {unloadedProductsCounter}/{countProductsInVehicle} products at {storageName}";
            return result;
        }

        public string GetStorageStatus(string storageName)
        {
            Storage storage = this.storages[storageName];
            Dictionary<string, int> productsAndCount = new Dictionary<string, int>();

            foreach (Product product in storage.Products)
            {
                string productType = product.GetType().Name;

                if (!productsAndCount.ContainsKey(productType))
                {
                    productsAndCount.Add(productType, 1);
                }
                else
                {
                    productsAndCount[productType]++;
                }
            }

            double productSum = storage.Products.Sum(p => p.Weight);
            int storageCapacity = storage.Capacity;

            string[] productsAsStrings = productsAndCount
                 .OrderByDescending(p => p.Value)
                 .ThenBy(p => p.Key)
                 .Select(kvp => $"{kvp.Key} ({kvp.Value})")
                 .ToArray();

            string stockLine = $"Stock ({productSum}/{storageCapacity}): [{string.Join(", ", productsAsStrings)}]";

            string[] storageStringsRepresentation = storage
                .Garage
                .Select(g => g == null ? "empty" : g.GetType().Name)
                .ToArray();

            string garageLine = $"Garage: [{string.Join("|", storageStringsRepresentation)}]";

            string result = stockLine +
                Environment.NewLine +
                garageLine;

            return result;

        }

        public string GetSummary()
        {
            Storage[] sortedStoragex = this.storages
                .Select(s => s.Value)
                .OrderByDescending(s => s.Products.Sum(p => p.Price))
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (Storage storage in sortedStoragex)
            {
                double totalMoney = storage.Products.Sum(p => p.Price);

                sb.AppendLine($"{storage.Name}:");
                sb.AppendLine($"Storage worth: ${totalMoney:F2}");
            }

            string result = sb.ToString().TrimEnd();
            return result;
        }

    }
}
