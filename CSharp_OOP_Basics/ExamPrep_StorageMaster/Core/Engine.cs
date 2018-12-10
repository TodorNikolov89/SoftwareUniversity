using StorageMaster.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Core
{
    public class Engine
    {
        private bool isRunning;
        private StorageMaster storageMaster;

        public Engine(StorageMaster  storageMaster)
        {
            this.storageMaster = new StorageMaster();
            this.isRunning = false;
        }
        public void Run()
        {
            this.isRunning = true;
            
            while (isRunning)
            {
                string input = Console.ReadLine();
                string[] line = input.Split();
                string command = line[0];
                string output = "";
                try
                {
                    switch (command)
                    {
                        case "AddProduct":
                            string type = line[1];
                            double price = double.Parse(line[2]);
                            output = this.storageMaster.AddProduct(type, price);
                            break;
                        case "RegisterStorage":
                            string registerStorageType = line[1];
                            string registerStorageName = line[2];
                            output = this.storageMaster.RegisterStorage(registerStorageType, registerStorageName);
                            break;
                        case "SelectVehicle":
                            string storageName = line[1];
                            int garageSlot = int.Parse(line[2]);
                            output = this.storageMaster.SelectVehicle(storageName, garageSlot);
                            break;
                        case "LoadVehicle":
                            string[] products = line.Skip(1).ToArray();
                            output = this.storageMaster.LoadVehicle(products);
                            break;
                        case "SendVehicleTo":
                            string sourceName = line[1];
                            int sourceGarageSlot = int.Parse(line[2]);
                            string destinationName = line[3];
                            output = this.storageMaster.SendVehicleTo(sourceName, sourceGarageSlot, destinationName);
                            break;
                        case "UnloadVehicle":
                            string UnloadVehicleStorageName = line[1];
                            int UnloadVehicleGarageSlot = int.Parse(line[2]);
                            output = this.storageMaster.UnloadVehicle(UnloadVehicleStorageName, UnloadVehicleGarageSlot);
                            break;
                        case "GetStorageStatus":
                            string stName = line[1];
                            output = this.storageMaster.GetStorageStatus(stName);
                            break;
                        case "END":
                            this.isRunning = false;
                            output = this.storageMaster.GetSummary();
                            break;
                    }

                }
                catch (InvalidOperationException ex)
                {

                    output = $"Error: {ex.Message}";
                }

                Console.WriteLine(output);

            }
        }
    }
}