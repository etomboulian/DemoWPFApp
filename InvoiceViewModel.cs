using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoWFPApp.Common;
using DemoWFPApp.Data;

namespace DemoWFPApp
{

    class InvoiceViewModel : INotifyPropertyChanged
    {
        // implement the notify property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Properties:
        // Base list for the listbox data
        private Invoice invoice;
        public BindingSource Invoice { get; }

        // Selected invoice detail for presentation in the main form
        private InvoiceDetail selectedInvoiceDetail;
        public InvoiceDetail SelectedInvoiceDetail
        {
            get { return selectedInvoiceDetail; }
            set
            {
                selectedInvoiceDetail =
                  new InvoiceDetail
                  {
                      DetailID = value.DetailID,
                      Quantity = value.Quantity,
                      Sku = value.Sku,
                      Description = value.Description,
                      Price = value.Price,
                      Taxable = value.Taxable
                  };
                NotifyPropertyChanged();
            }
        }
        
        // Invoice View Model constructor
        // gets the list, sets the list builds the binding source adaptor
        // sets the initial selectedInvoiceDetail
        public InvoiceViewModel()
        {
            invoice = InvoiceRepository.GetInvoice();
            Invoice = new BindingSource();
            Invoice.DataSource = invoice;
            SelectedInvoiceDetail = invoice[0];
        }

        // String formatted properties of the invoice class
        public string Subtotal 
        { 
            get { return decimalToMoney(invoice.Subtotal); } 
        }

        public string GST
        {
            get { return decimalToMoney(invoice.GST); }
        }

        public string PST
        {
            get { return decimalToMoney(invoice.PST); }
        }

        public string GrandTotal
        {
            get { return decimalToMoney(invoice.GrandTotal); }
        }

        public string AveragePrice
        {
            get { return decimalToMoney(invoice.AveragePrice); }
        }

        public string MaximumPrice
        {
            get { return decimalToMoney(invoice.MaximumPrice); }
        }

        public string MinimumPrice
        {
            get { return decimalToMoney(invoice.MinimumPrice); }
        }

        public int TotalItems 
        {
            get { return invoice.TotalItems; }
        }

        public int TotalTaxable
        {
            get { return invoice.TotalTaxable; }
        }

        public string MostExpensive
        {
            get { return invoice.MostExpensive; }
        }

        private string decimalToMoney(decimal value)
        {
            return string.Format("{0:C2}", value);
        }

        // function to fire event notifications to notify that label databindings may have changed
        internal void UpdateProperties()
        {
            NotifyPropertyChanged("SelectedInvoiceDetail");
            NotifyPropertyChanged("Invoice");

            NotifyPropertyChanged("Subtotal");
            NotifyPropertyChanged("GST");
            NotifyPropertyChanged("PST");
            NotifyPropertyChanged("GrandTotal");

            NotifyPropertyChanged("AveragePrice");
            NotifyPropertyChanged("MaximumPrice");
            NotifyPropertyChanged("MinimumPrice");

            NotifyPropertyChanged("TotalTaxable");
            NotifyPropertyChanged("MostExpensive");
        }

    }
}
