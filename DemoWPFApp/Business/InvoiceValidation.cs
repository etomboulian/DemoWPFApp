using DemoWFPApp.Common;
using DemoWFPApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoWFPApp.Business
{
    
    class InvoiceValidation
    {
        private static StringBuilder errorMessages;

        public static string GetErrorMessages()
        {
            return errorMessages.ToString();
        }

        public static Invoice GetInvoice()
        {
            return InvoiceRepository.GetInvoice();
        }

        public static int UpdateInvoice(InvoiceDetail detail, Invoice invoice)
        {
            int rowsAffected = 0;

            if(Validate(detail))
            {
                rowsAffected = InvoiceRepository.UpdateInvoice(detail, invoice);
            }
            else
            {
                rowsAffected = -1;
            }

            return rowsAffected;
        }


        public static bool Validate(InvoiceDetail invoiceDetail)
        {
            errorMessages = new StringBuilder();
            int errCount = 0;

            if(!ValidateQuantity(invoiceDetail))
            {
                ++errCount;
                errorMessages.AppendLine("Quantity cannot be negative");
            }

            if(!ValidateSku(invoiceDetail))
            {
                ++errCount;
                errorMessages.AppendLine("Sku must be of the format AA0000");
            }

            if(!ValidatePrice(invoiceDetail))
            {
                ++errCount;
                errorMessages.AppendLine("Price cannot be negative");
            }

            return errCount == 0;
        }

        public static bool ValidateQuantity(InvoiceDetail invoiceDetail)
        {
            return (invoiceDetail.Quantity < 0) ? false : true;
        }

        public static bool ValidateSku(InvoiceDetail invoiceDetail)
        {
            string skuRegex = "^[A-Z]{2}[0-9]{5}$";
            if(!string.IsNullOrEmpty(invoiceDetail.Sku))
            {
                return Regex.IsMatch(invoiceDetail.Sku, skuRegex) ? true : false;
            }

            return false;
        }

        public static bool ValidatePrice(InvoiceDetail invoiceDetail)
        {
            return (invoiceDetail.Price >= 0) ? true : false;
        }
    }
}
