using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class DataRepository : IRepository
    {
        //private List<Product> data = new List<Product>();
        private DataContext _context;

        public DataRepository(DataContext ctx) => _context = ctx;

        public IEnumerable<Product> Products => _context.Products
            .Include(p => p.Category)
            .ToArray();


        public PagedList<Product> GetProducts(QueryOptions options, long category = 0)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category);
            if(category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }
            return new PagedList<Product>(query, options);
        }


        public Product GetProduct(long key) => _context.Products
            .Include(p => p.Category)
            .First(p => p.Id == key);
        
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product p = _context.Products.Find(product.Id);
            p.Name = product.Name;
            p.CategoryId = product.CategoryId;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            _context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            Dictionary<long, Product> data = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = _context.Products.Where(p => data.Keys.Contains(p.Id));

            foreach(Product databaseProduct in baseline)
            {
                Product requestProduct = data[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }

            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
