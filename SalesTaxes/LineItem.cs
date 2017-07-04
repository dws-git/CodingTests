using System;
using System.Linq;

namespace SalesTaxes
{
    public class LineItem
    {
        public Product Item { get; set; }
        public Int32 Quantity { get; set; }

        public LineItem(Product product)
        {
            Item = product;
            foreach (var taxRule in Item.TaxRules)
            {
                taxRule.CalculateTax(this.Item);
            }
        }

        public decimal SalesTax()
        {
            return Item.TaxRules.Sum(x => Quantity * x.SalesTaxAmount);
        }

        public Decimal TotalCost()
        {
            return (Item.Price * Quantity) + SalesTax();
        }
        
    }
}
