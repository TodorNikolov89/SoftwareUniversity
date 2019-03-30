using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x => { x.AddProfile<CarDealerProfile>(); });

            var context = new CarDealerContext();



            //Query 9. Import Suppliers
            //var xmlString = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, xmlString));

            //Query 10. Import Parts
            //var xmlString = File.ReadAllText("../../../Datasets/parts.xml");
            //Console.WriteLine(ImportParts(context, xmlString));

            //Query 11. Import Cars
            //var xmlString = File.ReadAllText("../../../Datasets/cars.xml");
            //Console.WriteLine(ImportCars(context, xmlString));


            //Query 12. Import Customers
            //var xmlString = File.ReadAllText("../../../Datasets/customers.xml");
            //Console.WriteLine(ImportCustomers(context, xmlString));


            //Query 13. Import Sales
            //var xmlString = File.ReadAllText("../../../Datasets/sales.xml");
            //Console.WriteLine(ImportSales(context, xmlString));

            //Query 14. Cars With Distance
            //Console.WriteLine(GetCarsWithDistance(context));

            //Query 15. Cars from make BMW
            Console.WriteLine(GetCarsFromMakeBmw(context));

        }
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {//TODO
            var cars = context
                .Cars
                .Where(n => n.Make == "BMW")
                .Select(c => new CarBmwExportDto()
                {
                    Id = c.Id.ToString(),
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(m => m.Model)
                .ThenByDescending(d => d.TravelledDistance)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            ;
            XmlSerializer serializer = new XmlSerializer(typeof(CarBmwExportDto[]), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlNamespaces.Add(""," ");
            xmlNamespaces.Add(string.Empty, " ");
            serializer.Serialize(new StringWriter(sb), cars);

            string result = sb.ToString();

            return result;

        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(d => d.TravelledDistance > 2000000)
                .Select(c => new CarWithDistanceExport()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderBy(m => m.Make)
                .ThenBy(mo => mo.Model)
                .Take(10)
                .ToArray();


            StringBuilder sb = new StringBuilder();
            ;
            XmlSerializer serializer = new XmlSerializer(typeof(CarWithDistanceExport[]), new XmlRootAttribute("cars"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), cars, xmlNamespaces);

            string result = sb.ToString();

            return result;


        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SalesImportDto[]), new XmlRootAttribute("Sales"));
            var deserializedSales = (SalesImportDto[])serializer.Deserialize(new StringReader(inputXml));
            List<Sale> sales = new List<Sale>();

            var cars = context.Cars;

            foreach (var saleDto in deserializedSales)
            {
                if (!IsValid(saleDto))
                {
                    continue;
                }

                Sale sale = Mapper.Map<Sale>(saleDto);

                if (cars.Any(c => c.Id == sale.CarId))
                {
                    sales.Add(sale);
                }

            }

            int count = sales.Count();
            context.Sales.AddRange(sales);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CustomersImportDto[]), new XmlRootAttribute("Customers"));
            var deserializedCustomers = (CustomersImportDto[])serializer.Deserialize(new StringReader(inputXml));

            List<Customer> customers = new List<Customer>();

            foreach (var customerDto in deserializedCustomers)
            {
                if (!IsValid(customerDto))
                {
                    continue;
                }

                Customer customer = Mapper.Map<Customer>(customerDto);
                customers.Add(customer);
            }

            int count = customers.Count();
            context.Customers.AddRange(customers);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        //TODO incorrect
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CarsImportDto[]), new XmlRootAttribute("Cars"));
            var deserializedCars = (CarsImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var cars = deserializedCars.AsQueryable().ProjectTo<Car>().ToArray();

            var parts = context.Parts.ToArray();

            Random random = new Random();

            foreach (var car in cars)
            {
                int count = random.Next(10, 20);

                car.PartCars = new List<PartCar>();

                HashSet<Part> addedParts = new HashSet<Part>();

                for (int i = 0; i < count; i++)
                {
                    Part part;

                    do
                    {
                        part = parts[random.Next(parts.Length)];
                    }
                    while (addedParts.Contains(part));

                    addedParts.Add(part);

                    car.PartCars.Add(new PartCar
                    {
                        Part = part
                    });
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            string result = $"Successfully imported {cars.Count()}";

            return result;
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(PartsImportDto[]), new XmlRootAttribute("Parts"));
            var deserializedParts = (PartsImportDto[])serializer.Deserialize(new StringReader(inputXml));

            List<Part> parts = new List<Part>();
            var suppliers = context.Suppliers;

            foreach (var partDto in deserializedParts)
            {
                if (!IsValid(partDto))
                {
                    continue;
                }

                Part part = Mapper.Map<Part>(partDto);

                if (suppliers.Any(x => x.Id == part.SupplierId))
                {
                    parts.Add(part);
                }
            }

            int count = parts.Count;
            ;
            context.Parts.AddRange(parts);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SuppliersImportDto[]), new XmlRootAttribute("Suppliers"));
            var deserializedSuppliers = (SuppliersImportDto[])serializer.Deserialize(new StringReader(inputXml));

            List<Supplier> suppliers = new List<Supplier>();

            foreach (var supplierDto in deserializedSuppliers)
            {
                if (!IsValid(supplierDto))
                {
                    continue;
                }

                Supplier supplier = Mapper.Map<Supplier>(supplierDto);
                suppliers.Add(supplier);
            }

            int count = suppliers.Count;
            ;
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            string result = $"Successfully imported {count}";
            return result;
        }

        public static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}