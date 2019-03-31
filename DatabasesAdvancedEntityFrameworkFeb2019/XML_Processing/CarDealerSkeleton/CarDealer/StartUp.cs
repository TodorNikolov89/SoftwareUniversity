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
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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
            //Console.WriteLine(GetCarsFromMakeBmw(context));

            //Query 16. Local Suppliers
            //Console.WriteLine(GetLocalSuppliers(context));

            //Query 17. Cars with Their List of Parts
            //Console.WriteLine(GetCarsWithTheirListOfParts(context));

            //Query 18. Total Sales by Customer
            //Console.WriteLine(GetTotalSalesByCustomer(context));

            //Query 19. Sales with Applied Discount
            //Console.WriteLine(GetSalesWithAppliedDiscount(context));


        }
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    Model = s.Car.Model,
                    TravelledDistance = s.Car.TravelledDistance,
                    Make = s.Car.Make,
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car.PartCars.Sum(p => p.Part.Price),
                    PriceWithDiscount = s.Car.PartCars.Sum(p => p.Part.Price) - s.Car.PartCars.Sum(p => p.Part.Price) * s.Discount / 100
                });

            XDocument xmlDoc = new XDocument();
            XElement salesXml = new XElement("sales");

            foreach (var sale in sales)
            {
                XElement saleXml = new XElement("sale");
                XElement carXml = new XElement("car",
                    new XAttribute("make", sale.Make),
                    new XAttribute("model", sale.Model),
                    new XAttribute("travelled-distance", sale.TravelledDistance)
                    );
                XElement discountXml = new XElement("discount", sale.Discount);
                XElement customerXml = new XElement("customer-name", sale.CustomerName);
                XElement priceXml = new XElement("price", sale.Price);
                XElement priceWithDiscountXml = new XElement("price-with-discount", sale.PriceWithDiscount);
                saleXml.Add(carXml, discountXml, customerXml, priceXml, priceWithDiscountXml);


                salesXml.Add(saleXml);
            }
            xmlDoc.Add(salesXml);


            string result = xmlDoc.ToString();

            return result;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(c => new CarWithDistanceExport()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = context.PartCars.Where(s => s.CarId == c.Id).Select(p => new PartDto()
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList(),
                })
                .OrderByDescending(d => d.TravelledDistance)
                .ThenBy(m => m.Model)
                .Take(5)
                .ToList();

            XDocument xmlDoc = new XDocument();
            XElement carsXml = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carXml = new XElement("car",
                    new XAttribute("make", car.Make),
                    new XAttribute("model", car.Model),
                    new XAttribute("travelled-distance", car.TravelledDistance));


                XElement partsXml = new XElement("parts");
                foreach (var part in car.Parts)
                {
                    XElement partXml = new XElement("part",
                        new XAttribute("name", part.Name),
                        new XAttribute("price", part.Price));
                    partsXml.Add(partXml);
                }

                carXml.Add(partsXml);
                carsXml.Add(carXml);

            }

            xmlDoc.Add(carsXml);

            string result = xmlDoc.ToString();

            return result;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                            .Where(c => c.Sales.Count > 0)
                            .Select(x => new CustomerTotalSalesDto
                            {
                                Name = x.Name,
                                BougthCars = x.Sales.Count(),
                                SpentMoney = x.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                            })
                            .OrderByDescending(m => m.SpentMoney)
                            .ToArray();

            XDocument xmlDoc = new XDocument();
            XElement customersXml = new XElement("customers");
            ;
            foreach (var customer in customers)
            {
                XElement customerXml = new XElement("customer",
                    new XAttribute("full-name", customer.Name),
                    new XAttribute("bought-cars", customer.BougthCars),
                    new XAttribute("spent-money", customer.SpentMoney.ToString()));
                customersXml.Add(customerXml);
            }
            xmlDoc.Add(customersXml);


            string result = xmlDoc.ToString();

            return result;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new GetSuppliersDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();



            XDocument xmlDoc = new XDocument();
            XElement suppliersXml = new XElement("suppliers");

            foreach (var supplier in suppliers)
            {
                XElement supplierXml = new XElement("suplier",
                    new XAttribute("id", supplier.Id),
                        new XAttribute("name", supplier.Name),
                        new XAttribute("parts-count", supplier.PartsCount));
                suppliersXml.Add(supplierXml);
            }
            xmlDoc.Add(suppliersXml);


            string result = xmlDoc.ToString();
            return result;
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
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
                .ToList();


            XDocument xmlDoc = new XDocument();
            XElement carsXml = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carXml = new XElement("car",
                    new XAttribute("id", car.Id),
                        new XAttribute("model", car.Model),
                        new XAttribute("travelled-distance", car.TravelledDistance));
                carsXml.Add(carXml);
            }
            xmlDoc.Add(carsXml);


            string result = xmlDoc.ToString();
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

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToList();

            var cars = new List<Car>();

            var existingPartsIds = context.Parts
                .Select(x => x.Id)
                .ToArray();

            foreach (var x in carsParsed)
            {
                Car currentCar = new Car()
                {
                    Make = x.Element("make").Value,
                    Model = x.Element("model").Value,
                    TravelledDistance = Convert.ToInt64(x.Element("TraveledDistance").Value)
                };

                var partIds = new HashSet<int>();

                foreach (var id in x.Element("parts").Elements())
                {
                    var pid = Convert.ToInt32(id.Attribute("id").Value);
                    partIds.Add(pid);
                }

                foreach (var pid in partIds)
                {
                    PartCar currentPair = new PartCar()
                    {
                        Car = currentCar,
                        PartId = pid
                    };

                    if (existingPartsIds.Contains(pid) == false)
                        continue;

                    currentCar.PartCars.Add(currentPair);
                }

                cars.Add(currentCar);
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

