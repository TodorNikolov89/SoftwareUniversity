using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FastFood.Data;
using FastFood.Models;
using Newtonsoft.Json;
using FastFood.DataProcessor.Dto.Import;
using AutoMapper;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using FastFood.Models.Enums;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";
        private const string Success= "Order for {0} on {1} added";
        
        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            var positions = new HashSet<Position>();
            var employees = new List<Employee>();

            foreach (var dto in deserializedEmployees)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var position = positions.FirstOrDefault(p => p.Name.Equals(dto.Position, StringComparison.OrdinalIgnoreCase));

                if (position == null)
                {
                    position = new Position(dto.Position);

                    positions.Add(position);
                }

                Employee employee = new Employee()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Position = position
                };

                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var categories = new List<Category>();
            var items = new List<Item>();

            foreach (var dto in deserializedItems)
            {
                if (!IsValid(dto) || items.Any(i => i.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var category = categories.FirstOrDefault(n => n.Name.Equals(dto.Category, StringComparison.OrdinalIgnoreCase));

                if (category == null)
                {
                    category = new Category() { Name = dto.Category };
                    categories.Add(category);

                }

                var item = new Item()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Category = category
                };

                sb.AppendLine(string.Format(SuccessMessage, item.Name));
                items.Add(item);
            }

            context.Items.AddRange(items);
            context.SaveChanges();

            string result = sb.ToString();

            return result;
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var deserializedOrders = (OrderDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validOrders = new List<Order>();

            foreach (var orderDto in deserializedOrders)
            {
                if (!IsValid(orderDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var areAllItemsValid = true;
                var orderItems = new List<OrderItem>();
                foreach (var oi in orderDto.Items)
                {
                    var item = context.Items
                        .FirstOrDefault(i => i.Name.Equals(oi.Name, StringComparison.OrdinalIgnoreCase));

                    if (!IsValid(oi) || item == null)
                    {
                        areAllItemsValid = false;
                        break;
                    }

                    var orderItem = new OrderItem
                    {
                        ItemId = item.Id,
                        Quantity = oi.Quantity
                    };

                    orderItems.Add(orderItem);
                }

                var employee = context.Employees
                    .FirstOrDefault(e => e.Name.Equals(orderDto.Employee, StringComparison.OrdinalIgnoreCase));

                DateTime date;
                var isDateValid = DateTime.TryParseExact(orderDto.DateTime,
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out date);

                if (employee == null || !areAllItemsValid || !isDateValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var order = new Order
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    DateTime = date,
                    Type = orderDto.Type,
                    OrderItems = orderItems
                };

                sb.AppendLine(string.Format(Success,
                    order.Customer,
                    order.DateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)));

                validOrders.Add(order);
            }

            context.Orders.AddRange(validOrders);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }
        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationresult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
            return isValid;
        }
    }
}