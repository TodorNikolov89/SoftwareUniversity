using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO.Export;
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
            //var jsonString = File.ReadAllText(@"../../../Datasets/customers.json");
            //Console.WriteLine(ImportCustomers(context, jsonString));

            //Query 13. Import Sales
            //var jsonString = File.ReadAllText(@"../../../Datasets/sales.json");
            //Console.WriteLine(ImportSales(context, jsonString));

            //Query 14. Export Ordered Customers
            //Console.WriteLine(GetOrderedCustomers(context));

            //Query 15. Export Cars from make Toyota
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            //Query 16. Export Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(context));

            //Query 17. Export Cars with Their List of Parts
            Console.WriteLine(GetCarsWithTheirListOfParts(context));

            //Query 18. Export Total Sales by Customer
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            //Query 19. Export Sales with Applied Discount
            //Console.WriteLine(GetSalesWithAppliedDiscount(context));

        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //var sales = context.Sales
            //    .Select(c => new SaleDto
            //    {
            //        Car = new CarSaleDto
            //        {
            //            Make = c.Car.Make,
            //            Model = c.Car.Model,
            //            TravelledDistance = c.Car.TravelledDistance
            //        },
            //        CustomerName = c.Customer.Name,
            //        Discount = c.Discount,
            //        Price = c.Car.PartCars.Sum(s => s.Part.Price),
            //        PriceWithDiscount = c.Car.PartCars.Sum(s => s.Part.Price) - c.Car.PartCars.Sum(s => s.Part.Price) * (c.Discount / 100)
            //    })
            //    .Take(10)
            //    .ToArray();

            //string jsonString = JsonConvert.SerializeObject(sales, Formatting.Indented);

            return null;

        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count(),
                    spentMoney = x.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .ToArray();

            string jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return jsonCustomers;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            //TODO
            var cars = context
                .Cars
                .Select(c => new CarExportDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                        .Select(pc => new PartExportDto()
                        {
                            Name = pc.Part.Name,
                            Price = pc.Part.Price
                        })
                        .ToArray()
                })
                .ToArray();


            var jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return jsonCars;

        }


        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count()
                })
                .ToArray();

            string jsonSuppliers = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return jsonSuppliers;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(cm => cm.Make == "Toyota")
                .OrderBy(m => m.Model)
                .ThenByDescending(d => d.TravelledDistance)
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToArray();

            string jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return jsonCars;
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(b => b.BirthDate)
                .ThenBy(i => i.IsYoungDriver)
                .Select(c => new
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToArray();

            string jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return jsonCustomers;
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);
            var salesCount = sales.Count();

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
            var cars = JsonConvert.DeserializeObject<Car[]>(inputJson);
            int carsCount = cars.Count();

            context.Cars.AddRange(cars);
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