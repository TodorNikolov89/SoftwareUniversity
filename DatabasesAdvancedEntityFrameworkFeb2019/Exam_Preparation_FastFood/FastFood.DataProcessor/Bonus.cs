using System;
using System.Linq;
using FastFood.Data;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
        public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
        {
            var item = context.Items.FirstOrDefault(n => n.Name == itemName);

            if (item == null)
            {
                return $"Item {itemName} not found!";
            }

            var oldPrice = item.Price;
            item.Price = newPrice;

            return $"Cheeseburger Price updated from ${oldPrice} to ${newPrice}";

        }
    }
}
