namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Data;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class StartUp
    {
        public static void Main(string[] args)
        {

            Mapper.Initialize(x => { x.AddProfile<ProductShopProfile>(); });

            var context = new ProductShopContext();

            //Query 1. Import Users
            //var xmlString = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(context, xmlString));

            //Query 2. Import Products
            //var xmlString = File.ReadAllText("../../../Datasets/products.xml");
            //Console.WriteLine(ImportProducts(context, xmlString));

            //Query 3. Import Categories
            //var xmlString = File.ReadAllText("../../../Datasets/categories.xml");
            //Console.WriteLine(ImportCategories(context, xmlString));

            //Query 4. Import Categories and Products
            //var xmlString = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, xmlString));

            //Query 5. Products In Range
            //Console.WriteLine(GetProductsInRange(context));

            //Query 6. Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            //Query 7. Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            //Query 8. Users and Products
            Console.WriteLine(GetUsersWithProducts(context));

        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new UsersDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new UserSoldProducts
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(ps => new SoldProductsDto
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToList()
                    }
                })
                .OrderByDescending(p => p.SoldProducts.Count)
                .Take(10)
                .ToList();

            var custom = new UserProductsInfoDto
            {
                Count = context.Users.Count(x=>x.ProductsSold.Any()),
                Users = users
            };
            
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(UserProductsInfoDto), new XmlRootAttribute("Users"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), custom, xmlNamespaces);

            string result = sb.ToString();

            return result;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new CategoryDtoExport()
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count(),
                    AveragePrice = c.CategoryProducts.Average(x => x.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(x => x.Product.Price),

                })
                .OrderByDescending(c => c.Count)
                .ThenBy(t => t.TotalRevenue)
                .ToArray();
            StringBuilder sb = new StringBuilder();
            ;
            XmlSerializer serializer = new XmlSerializer(typeof(Dtos.Export.CategoryDtoExport[]), new XmlRootAttribute("Categories"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);

            string result = sb.ToString();

            return result;

        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(l => l.LastName)
                .ThenBy(f => f.FirstName)
                .Select(u => new UserDtoExport()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Products = u.ProductsSold.Select(p => new SoldProductsDto()
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList()
                })
                .Take(5)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(Dtos.Export.UserDtoExport[]), new XmlRootAttribute("Users"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);

            string result = sb.ToString();

            return result;

        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000 && p.Buyer != null)
                .OrderBy(p => p.Price)
                .Select(p => new ProductDtoExport
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}" ?? p.Buyer.LastName
                })
                .Take(10)
                .ToArray();

            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(Dtos.Export.ProductDtoExport[]), new XmlRootAttribute("Products"));
            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), products, xmlNamespaces);


            string result = sb.ToString();

            return result;
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CategoryProductDto[]), new XmlRootAttribute("CategoryProducts"));
            var deserializedCatProd = (CategoryProductDto[])serializer.Deserialize(new StringReader(inputXml));

            List<CategoryProduct> categoryProducts = new List<CategoryProduct>();

            var categories = context.Categories;
            var products = context.Products;

            foreach (var categoryProductDto in deserializedCatProd)
            {
                if (!IsValid(categoryProductDto))
                {
                    continue;
                }

                if (!products.Any(x => x.Id == categoryProductDto.ProductId)
                    || !categories.Any(x => x.Id == categoryProductDto.CategoryId))
                {
                    continue;
                }

                var categoryProduct = Mapper.Map<CategoryProduct>(categoryProductDto);

                categoryProducts.Add(categoryProduct);
            }

            int count = categoryProducts.Count;

            context.AddRange(categoryProducts);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serialiser = new XmlSerializer(typeof(Dtos.Import.CategoryDto[]), new XmlRootAttribute("Categories"));
            var deserializedCategories = (Dtos.Import.CategoryDto[])serialiser.Deserialize(new StringReader(inputXml));

            List<Category> categories = new List<Category>();

            foreach (var categoryDto in deserializedCategories)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                var category = Mapper.Map<Category>(categoryDto);

                categories.Add(category);
            }

            int count = categories.Count;

            context.Categories.AddRange(categories);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(Dtos.Import.ProductDto[]), new XmlRootAttribute("Products"));
            var deserializedProducts = (Dtos.Import.ProductDto[])serializer.Deserialize(new StringReader(inputXml));

            List<Product> products = new List<Product>();

            foreach (var productDto in deserializedProducts)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                Product product = Mapper.Map<Product>(productDto);

                products.Add(product);
            }

            int count = products.Count;

            context.Products.AddRange(products);
            context.SaveChanges();

            string result = $"Successfully imported {count}";

            return result;
        }

        public static string ImportUsers(ProductShopContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(Dtos.Import.UserDto[]), new XmlRootAttribute("Users"));
            var deserializedUsers = (Dtos.Import.UserDto[])serializer.Deserialize(new StringReader(xmlString));

            List<User> users = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                User user = Mapper.Map<User>(userDto);
                users.Add(user);
            }

            int count = users.Count;

            context.Users.AddRange(users);
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