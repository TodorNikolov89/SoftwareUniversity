namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using AutoMapper;
    using Newtonsoft.Json;
    using ProductShop.Data;
    using ProductShop.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();

            var context = new ProductShopContext();

            //Query 1. Import Users
            //var jsonString = File.ReadAllText(@"../../../Datasets/users.json");
            //Console.WriteLine(ImportUsers(context, jsonString));

            //Query 2. Import Products
            //var jsonString = File.ReadAllText(@"../../../Datasets/products.json");
            //Console.WriteLine(ImportProducts(context, jsonString));

            //Query 3. Import Categories
            //var jsonString = File.ReadAllText(@"../../../Datasets/categories.json");
            //Console.WriteLine(ImportCategories(context, jsonString));

            //Query 4. Import Categories and Products
            //var jsonString = File.ReadAllText(@"../../../Datasets/categories-products.json");
            //Console.WriteLine(ImportCategoryProducts(context, jsonString));

            //Query 5. Export Products In Range
            //Console.WriteLine(GetProductsInRange(context));

            //Query 6. Export Successfully Sold Products
            //Console.WriteLine(GetSoldProducts(context));

            //Query 7. Export Categories By Products Count
            //Console.WriteLine(GetCategoriesByProductsCount(context));

            //Query 8. Export Users and Products
            //Console.WriteLine(GetUsersWithProducts(context));
        }
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var fillteredUsers = context.Users
                .Where(x => x.ProductsSold.Count > 0 && x.ProductsSold.Any(c => c.Buyer != null))
                .OrderByDescending(s => s.ProductsSold.Count(ps => ps.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    age = x.Age,
                    soldProducts = new
                    {
                        count = x.ProductsSold.Count(ps => ps.Buyer != null),
                        products = x.ProductsSold.Where(ps => ps.Buyer != null)
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        })
                        .ToArray()
                    }
                })
                .ToArray();

            var jsonString = new
            {
                usersCount = fillteredUsers.Length,
                users = fillteredUsers
            };

            return JsonConvert.SerializeObject(jsonString, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.CategoryProducts.Count,
                    averagePrice = $"{x.CategoryProducts.Sum(s => s.Product.Price) / x.CategoryProducts.Count:f2}",
                    totalRevenue = $"{x.CategoryProducts.Sum(s => s.Product.Price):f2}"
                })
                .OrderByDescending(x => x.productsCount)
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return jsonString;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(s => s.ProductsSold.Count > 0 && s.ProductsSold.Any(c => c.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold.Where(a => a.Buyer != null).Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    })
                    .ToArray()
                })
                 .OrderBy(l => l.lastName)
                .ThenBy(f => f.firstName)
                .ToArray();

            var jsonProducts = JsonConvert.SerializeObject(users, Formatting.Indented);

            return jsonProducts;
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(x => x.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName ?? x.Seller.LastName
                })
                .ToArray();

            var jsonProduct = JsonConvert.SerializeObject(products, Formatting.Indented);

            return jsonProduct;
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var deserializedCategoriesAndProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
            var categoriesAndProductsCount = deserializedCategoriesAndProducts.Count();

            context.CategoryProducts.AddRange(deserializedCategoriesAndProducts);
            context.SaveChanges();

            string result = $"Successfully imported {categoriesAndProductsCount}";

            return result;

        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {

            var deserializedCategories = JsonConvert.DeserializeObject<Category[]>(inputJson).Where(x => x.Name != null);
            int categoriesCount = deserializedCategories.Count();

            context.Categories.AddRange(deserializedCategories);
            context.SaveChanges();

            string result = $"Successfully imported {categoriesCount}";

            return result;
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var deserializedProducts = JsonConvert.DeserializeObject<Product[]>(inputJson);
            int productsCount = deserializedProducts.Count();

            context.Products.AddRange(deserializedProducts);
            context.SaveChanges();

            string result = $"Successfully imported {productsCount}";

            return result;
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(inputJson);
            int usersCount = deserializedUsers.Count();

            context.Users.AddRange(deserializedUsers);
            context.SaveChanges();
            string result = $"Successfully imported {usersCount}";
            return result;
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }
    }
}