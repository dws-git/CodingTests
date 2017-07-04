using System;

namespace SalesTaxes
{
    public class BasicTax : TaxRule
    {
        public BasicTax()
        {
            SalesTaxRate = 0.10m;           
        }

        public override decimal CalculateTax(Product product)
        {
            SalesTaxAmount = Math.Ceiling(Decimal.Round(product.Price * SalesTaxRate, 2) / .05m) * 0.05m;
            return SalesTaxAmount;
        }

    }
}