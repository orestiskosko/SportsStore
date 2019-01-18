using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class WebServiceRepository : IWebServiceRepository
    {
        private DataContext _context;

        public WebServiceRepository(DataContext ctx) => _context = ctx;

        public object GetProduct(long id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.PurchasePrice,
                    p.RetailPrice,
                    Category = new
                    {
                        p.Category.Id,
                        p.Category.Name,
                        p.Category.Description
                    }
                })
                .FirstOrDefault(p => p.Id == id);
        }

        public object GetProducts(int skip, int take)
        {
            return _context.Products.Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.RetailPrice,
                    p.PurchasePrice,
                    p.CategoryId,
                    Category = new
                    {
                        p.Category.Name,
                        p.Category.Description
                    }
                });
        }

        public long StoreProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();       
        }


        public void DeleteProduct(long id)
        {
            _context.Products.Remove(new Product { Id = id });
            _context.SaveChanges();
        }
    }
}
