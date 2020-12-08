using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWFPApp.Common
{
    class Invoice : List<InvoiceDetail>
    {

        public decimal Subtotal => this.Sum(e => e.Extended);

        public decimal GST => this.Sum(e => (e.Extended * 0.05m));

        public decimal PST => this.Where(e => e.Taxable == true).Sum(e => (e.Extended * 0.07m));

        public decimal GrandTotal => (Subtotal + GST + PST);

        public decimal AveragePrice => this.Sum(i => i.Price) / Count;

        public decimal MaximumPrice => this.Max(i => i.Price);
        public decimal MinimumPrice => this.Min(i => i.Price);
        public int TotalItems => this.Count;
        public int TotalTaxable => this.Where(i => i.Taxable == true).Count();

        public string MostExpensive
        {
            get 
            {
                InvoiceDetail maxItem = new InvoiceDetail();
                foreach(InvoiceDetail item in this)
                {
                    if(item.Price > maxItem.Price)
                    {
                        maxItem = item;
                    }
                }

                return maxItem.Sku;
            }
        }

    }
}
