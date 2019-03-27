using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            var mapper = config.CreateMapper();

            var context = new CarDealerContext();


            //Query 9. Import Suppliers
            //var jsonString = File.ReadAllText(@"../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(context, jsonString));

            //TODO
            //Query 10. Import Parts 
            //var jsonString = File.ReadAllText(@"../../../Datasets/parts.json");
            //Console.WriteLine(ImportParts(context, jsonString));

            //TODO
            //Query 11. Import Cars
            //var jsonString = File.ReadAllText(@"../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(context, jsonString));

            //Query 12. Import Customers
            // var jsonString = File.ReadAllText(@"../../../Datasets/customers.json");
            //Console.WriteLine(ImportCars(context, jsonString));

            //Query 13. Import Sales
            var jsonString = File.ReadAllText(@"../../../Datasets/sales.json");
            Console.WriteLine(ImportSales(context, jsonString));
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);
            var salesCount = sales.Count();

            var cars = context.Cars;

            context.Sales.AddRange(sales);
            context.SaveChanges();

            string result = $"Successfully imported {salesCount}.";
            return result;
        }


        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);
            int carsCount = customers.Count();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            string result = $"Successfully imported {carsCount}.";
            return result;
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            //TODO
            var deserializedCars = JsonConvert.DeserializeObject<Car[]>(inputJson);
            int carsCount = deserializedCars.Count();

            context.Cars.AddRange(deserializedCars);
            context.SaveChanges();

            string result = $"Successfully imported {carsCount}.";
            return result;
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<Part[]>(inputJson);
            var suppliers = context.Suppliers.ToArray();

            int partscount = 0;

            foreach (var part in parts)
            {
                if (suppliers.Any(s => s.Id == part.SupplierId))
                {
                    partscount++;
                    context.Add(part);                    
                }
            }
            context.SaveChanges();

            string result = $"Successfully imported {partscount}.";

            return result;
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var deserializedSuppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);
            int suppliersCount = deserializedSuppliers.Count();

            context.Suppliers.AddRange(deserializedSuppliers);
            context.SaveChanges();
            string result = $"Successfully imported {suppliersCount}.";
            return result;
        }


    }
}