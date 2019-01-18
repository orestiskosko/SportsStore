using SportsStore.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }

        Product GetProduct(long key);

        PagedList<Product> GetProducts(QueryOptions options, long category = 0);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void UpdateAll(Product[] products);

        void Delete(Product product);
    }
}
