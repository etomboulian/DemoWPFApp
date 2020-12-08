using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWFPApp.Common
{
    class InvoiceDetail : IComparable<InvoiceDetail>
    {

        public int DetailID { get; set; }
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Extended
        {
            get { return Quantity * Price; }
        }
        public bool Taxable { get; set; }

        public int CompareTo(InvoiceDetail other)
        {
            if(other == null)
            {
                return 1;
            }
            else
            {
                return string.Compare(this.Sku, other.Sku);
            }

        }
    }
}
