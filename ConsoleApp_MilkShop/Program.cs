using MilkShop.common;
using MilkShopBusiness.Base;
using MilkShopData.Models;
using System;
using System.Threading.Tasks;

namespace MilkShopConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create an instance of CategoryBusiness
            CategoryBusiness categoryBusiness = new CategoryBusiness();

            // Create a new category object
            Category newCategory = new Category
            {
                CategoryId = 4,
                CategoryName = "Dairy Products",
                Type = "All kinds of dairy products"
            };

            // Call the Save method and get the result
            IMilkShopResult result = await categoryBusiness.Save(newCategory);

            // Output the result
            Console.WriteLine($"Result Code: {result.Status}");
            Console.WriteLine($"Message: {result.Message}");
        }
    }
}
