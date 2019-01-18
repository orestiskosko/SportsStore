using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasIndex(p => p.Name);
            builder.Entity<Product>().HasIndex(p => p.PurchasePrice);
            builder.Entity<Product>().HasIndex(p => p.RetailPrice);

            builder.Entity<Category>().HasIndex(c => c.Name);
            builder.Entity<Category>().HasIndex(c => c.Description);
        }
    }
}
