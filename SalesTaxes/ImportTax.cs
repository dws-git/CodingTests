using System;

namespace SalesTaxes
{
    public class ImportTax : TaxRule
    {
        public ImportTax()
        {
            SalesTaxRate = 0.05m;
        }

        public override decimal CalculateTax(Product product)
        {
            SalesTaxAmount = Math.Ceiling(Decimal.Round(product.Price * SalesTaxRate, 2) / .05m) * 0.05m;
            return SalesTaxAmount;
        }

    }
}