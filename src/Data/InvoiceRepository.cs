using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoWPFApp.Common;

namespace DemoWPFApp.Data
{
    class InvoiceRepository
    {

        public static Invoice GetInvoice()
        {
            Invoice invoice = new Invoice();

            InvoiceDetail[] details =
            {   new InvoiceDetail
                {   
                    DetailID = 1,
                    Quantity = 5,
                    Sku = "AA01234",
                    Description = "Description Test1",
                    Price = 1.00m,
                    Taxable = true 
                },
                new InvoiceDetail
                {
                    DetailID = 2,
                    Quantity = 2,
                    Sku = "AA01247",
                    Description = "Description Test2",
                    Price = 25.00m,
                    Taxable = false
                },
                new InvoiceDetail
                {
                    DetailID = 3,
                    Quantity = 15,
                    Sku = "AA03734",
                    Description = "Description Test3",
                    Price = 10.00m,
                    Taxable = true
                }
            };

            foreach (InvoiceDetail item in details)
            {
                invoice.Add(item);
            }

            return invoice;
        }

        public static int UpdateInvoice(InvoiceDetail detail, Invoice invoice)
        {
            int rowsAffected = -1;

            for(int i = 0; i < invoice.Count; ++i)
            {
                if(invoice[i].DetailID == detail.DetailID)
                {
                    invoice[i] = detail;
                    rowsAffected = 1;
                    break;
                }
            }

            return rowsAffected;
        }
    }
}
