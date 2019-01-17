using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }

        //
        // Establish relationship with Categories table
        //

        // Foreign Key property
        // - Naming convention [class name][primary key property name] =  [Category][Id]
        public long CategoryId { get; set; }

        // Navigation property 
        // - EF will populate this property with the Category object that is identified by the foreign key property.
        public Category Category { get; set; }
    }
}
