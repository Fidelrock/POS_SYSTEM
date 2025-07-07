using POS_SYSTEM.Data;
using POS_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POS_SYSTEM.Services
{
    public class DatabaseService
    {
        // Product CRUD
        public List<Product> GetAllProducts()
        {
            using (var context = new SupermarketContext())
            {
                return context.Products.Include(p => p.Category).ToList();
            }
        }

        public Product GetProductById(int productId)
        {
            using (var context = new SupermarketContext())
            {
                return context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
            }
        }

        public Product GetProductByBarcode(string barcode)
        {
            using (var context = new SupermarketContext())
            {
                return context.Products.Include(p => p.Category).FirstOrDefault(p => p.Barcode == barcode);
            }
        }

        public void AddProduct(Product product)
        {
            using (var context = new SupermarketContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new SupermarketContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var context = new SupermarketContext())
            {
                var product = context.Products.Find(productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
        }

        // Category CRUD
        public List<Category> GetAllCategories()
        {
            using (var context = new SupermarketContext())
            {
                return context.Categories.Include(c => c.Products).ToList();
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var context = new SupermarketContext())
            {
                return context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == categoryId);
            }
        }

        public void AddCategory(Category category)
        {
            using (var context = new SupermarketContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new SupermarketContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (var context = new SupermarketContext())
            {
                var category = context.Categories.Find(categoryId);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
            }
        }

        // Sale CRUD
        public List<Sale> GetAllSales()
        {
            using (var context = new SupermarketContext())
            {
                return context.Sales.Include(s => s.SaleItems.Select(si => si.Product)).ToList();
            }
        }

        public Sale GetSaleById(int saleId)
        {
            using (var context = new SupermarketContext())
            {
                return context.Sales.Include(s => s.SaleItems.Select(si => si.Product)).FirstOrDefault(s => s.SaleId == saleId);
            }
        }

        public void AddSale(Sale sale, List<SaleItem> saleItems)
        {
            using (var context = new SupermarketContext())
            {
                sale.SaleItems = saleItems;
                context.Sales.Add(sale);
                context.SaveChanges();
            }
        }

        public void UpdateSale(Sale sale)
        {
            using (var context = new SupermarketContext())
            {
                context.Entry(sale).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteSale(int saleId)
        {
            using (var context = new SupermarketContext())
            {
                var sale = context.Sales.Find(saleId);
                if (sale != null)
                {
                    context.Sales.Remove(sale);
                    context.SaveChanges();
                }
            }
        }

        // SaleItem CRUD
        public List<SaleItem> GetSaleItemsBySaleId(int saleId)
        {
            using (var context = new SupermarketContext())
            {
                return context.SaleItems.Include(si => si.Product).Where(si => si.SaleId == saleId).ToList();
            }
        }

        public void AddSaleItem(SaleItem saleItem)
        {
            using (var context = new SupermarketContext())
            {
                context.SaleItems.Add(saleItem);
                context.SaveChanges();
            }
        }

        public void UpdateSaleItem(SaleItem saleItem)
        {
            using (var context = new SupermarketContext())
            {
                context.Entry(saleItem).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteSaleItem(int saleItemId)
        {
            using (var context = new SupermarketContext())
            {
                var saleItem = context.SaleItems.Find(saleItemId);
                if (saleItem != null)
                {
                    context.SaleItems.Remove(saleItem);
                    context.SaveChanges();
                }
            }
        }
    }
}