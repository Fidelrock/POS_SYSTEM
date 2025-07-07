using POS_SYSTEM.Data;
using System.Data.Entity;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace POS_SYSTEM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SupermarketContext>());

            // Seed sample products if not already present
            using (var context = new SupermarketContext())
            {
                if (!context.Products.Any())
                {
                    var category = context.Categories.FirstOrDefault();
                    if (category == null)
                    {
                        category = new Models.Category { Name = "Beverages" };
                        context.Categories.Add(category);
                        context.SaveChanges();
                    }

                    var products = new List<Models.Product>
                    {
                        new Models.Product { Barcode = "100000001", Name = "Apple Juice", CategoryId = category.CategoryId, Price = 2.50m, Stock = 20 },
                        new Models.Product { Barcode = "100000002", Name = "Orange Juice", CategoryId = category.CategoryId, Price = 2.75m, Stock = 15 },
                        new Models.Product { Barcode = "100000003", Name = "Cola", CategoryId = category.CategoryId, Price = 1.50m, Stock = 30 },
                        new Models.Product { Barcode = "100000004", Name = "Lemonade", CategoryId = category.CategoryId, Price = 1.80m, Stock = 25 },
                        new Models.Product { Barcode = "100000005", Name = "Iced Tea", CategoryId = category.CategoryId, Price = 2.00m, Stock = 18 },
                        new Models.Product { Barcode = "100000006", Name = "Mineral Water", CategoryId = category.CategoryId, Price = 1.00m, Stock = 50 },
                        new Models.Product { Barcode = "100000007", Name = "Grape Juice", CategoryId = category.CategoryId, Price = 2.60m, Stock = 12 },
                        new Models.Product { Barcode = "100000008", Name = "Pineapple Juice", CategoryId = category.CategoryId, Price = 2.90m, Stock = 10 },
                        new Models.Product { Barcode = "100000009", Name = "Energy Drink", CategoryId = category.CategoryId, Price = 3.20m, Stock = 8 },
                        new Models.Product { Barcode = "100000010", Name = "Tomato Juice", CategoryId = category.CategoryId, Price = 2.30m, Stock = 14 }
                    };
                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }

            using (var context = new SupermarketContext())
            {
                var categories = context.Categories.ToList(); // This will trigger DB creation
            }

            base.OnStartup(e);
        }
    }
}