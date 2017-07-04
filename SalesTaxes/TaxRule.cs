namespace SalesTaxes
{
    public abstract class TaxRule
    {
        public decimal SalesTaxRate { get; set; }
        public decimal SalesTaxAmount { get; set; }

        public abstract decimal CalculateTax(Product product);

    }
}
