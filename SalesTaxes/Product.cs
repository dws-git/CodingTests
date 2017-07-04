using System.Collections.Generic;

namespace SalesTaxes
{
    public class Product
    {
        public Product(string desc, decimal price)
        {
            Description = desc;
            Price = price;
            TaxRules = new List<TaxRule>();
        }     

        public decimal Price { get; }
        public string Description { get; }
        public List<TaxRule> TaxRules { get; set; }
        
    }
}