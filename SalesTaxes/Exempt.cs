namespace SalesTaxes
{
    public class Exempt : TaxRule
    {
        public Exempt()
        {
            SalesTaxRate = 0.0m;
            SalesTaxAmount = 0.0m;
        }

        public override decimal CalculateTax(Product product)
        {
            return SalesTaxAmount;
        }

    }
}