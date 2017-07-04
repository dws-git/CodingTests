using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SalesTaxes
{
    public class ShoppingCart
    {
        public ConcurrentQueue<LineItem> LineItems = new ConcurrentQueue<LineItem>();

        public void Add(Product product)
        {
            if (!LineItems.Any())
            {
                LineItems.Enqueue(new LineItem(product) {Quantity = 1});
                return;
            }
            foreach (var lineItem in LineItems)
            {
                if (lineItem.Item.Description == product.Description)
                {
                    lineItem.Quantity++;
                    return;
                }
            }
            LineItems.Enqueue(new LineItem(product) { Quantity = 1 });
        }

        public void PrintLineItems()
        {
            foreach (var item in LineItems)
            {
                Console.WriteLine($"{item.Quantity} {item.Item.Description}: {item.Item.Price + item.SalesTax()}");
            }
        }

        public void PrintSalesTax()
        {
            Console.WriteLine($"Sales Taxes: {LineItems.Sum(item => item.SalesTax())}");
        }

        public void PrintTotalCost()
        {
            Console.WriteLine($"Total: {LineItems.Sum(item => item.TotalCost())}");
        }
    }
}

