using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace SalesTaxes.Tests
{
    public class UnitTests : IDisposable
    {
        private readonly ITestOutputHelper output;

        public UnitTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void InputOne()
        {   
            //create product instances and add tax rules
            var book = new Product("book", 12.49m);
            book.TaxRules.Add(new Exempt());
            var cd = new Product("music CD", 14.99m);
            cd.TaxRules.Add(new BasicTax());
            var chocolate = new Product("chocolate bar", 0.85m);
            chocolate.TaxRules.Add(new Exempt());

            //add products to shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(book);
            cart.Add(cd);
            cart.Add(chocolate);

            //redirect console output and assert it is as expected
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.WriteLine("Output 1:");
                cart.PrintLineItems();
                cart.PrintSalesTax();
                cart.PrintTotalCost();
                var expectedOutput =
                    String.Format(
                        "Output 1:{0}1 book: 12.49{0}1 music CD: 16.49{0}1 chocolate bar: 0.85{0}Sales Taxes: 1.50{0}Total: 29.83{0}",
                        Environment.NewLine);
                string actual = writer.ToString();
                Assert.Equal(expectedOutput, actual);
                output.WriteLine(actual);
            }
        }

        [Fact]
        public void InputTwo()
        {
            //create product instances and add tax rules
            var importedBoxChocolates = new Product("imported box of chocolates", 10.00m);
            importedBoxChocolates.TaxRules.Add(new Exempt());
            importedBoxChocolates.TaxRules.Add(new ImportTax());
            var importedBottlePerfume = new Product("imported bottle of perfume", 47.50m);
            importedBottlePerfume.TaxRules.Add(new BasicTax());
            importedBottlePerfume.TaxRules.Add(new ImportTax());

            //add products to shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(importedBoxChocolates);
            cart.Add(importedBottlePerfume);

            //redirect console output and assert it is as expected
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.WriteLine("Output 2:");
                cart.PrintLineItems();
                cart.PrintSalesTax();
                cart.PrintTotalCost();
                var expectedOutput =
                    String.Format(
                        "Output 2:{0}1 imported box of chocolates: 10.50{0}1 imported bottle of perfume: 54.65{0}Sales Taxes: 7.65{0}Total: 65.15{0}",
                        Environment.NewLine);
                string actual = writer.ToString();
                Assert.Equal(expectedOutput, actual);
                output.WriteLine(actual);
            }
        }


        [Fact]
        public void InputThree()
        {
            //create product instances and add tax rules
            var importedPerfume = new Product("imported bottle of perfume", 27.99m);
            importedPerfume.TaxRules.Add(new BasicTax());
            importedPerfume.TaxRules.Add(new ImportTax());
            var perfume = new Product("bottle of perfume", 18.99m);
            perfume.TaxRules.Add(new BasicTax());
            var headachePills = new Product("packet of headache pills", 9.75m);
            headachePills.TaxRules.Add(new Exempt());
            var importedBoxChocolates = new Product("imported box of chocolates", 11.25m);
            importedBoxChocolates.TaxRules.Add(new ImportTax());

            //add products to shopping cart
            ShoppingCart cart = new ShoppingCart();
            cart.Add(importedPerfume);
            cart.Add(perfume);
            cart.Add(headachePills);
            cart.Add(importedBoxChocolates);

            //redirect console output and assert it is as expected
            using (StringWriter writer = new StringWriter())
            {
                Console.SetOut(writer);
                Console.WriteLine("Output 3:");
                cart.PrintLineItems();
                cart.PrintSalesTax();
                cart.PrintTotalCost();
                var expectedOutput =
                    String.Format(
                        "Output 3:{0}1 imported bottle of perfume: 32.19{0}1 bottle of perfume: 20.89{0}1 packet of headache pills: 9.75{0}1 imported box of chocolates: 11.85{0}Sales Taxes: 6.70{0}Total: 74.68{0}",
                        Environment.NewLine);
                string actual = writer.ToString();
                Assert.Equal(expectedOutput, actual);
                output.WriteLine(actual);
            }
        }

        private void ResetConsole()
        {
            StreamWriter standardOut = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOut);
        }

        public void Dispose()
        {
            ResetConsole();
            GC.SuppressFinalize(this);
        }

        ~UnitTests()
        {
            ResetConsole();
        }
    }
}
