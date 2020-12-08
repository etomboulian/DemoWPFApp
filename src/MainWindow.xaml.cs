using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoWPFApp.Business;
using DemoWPFApp.Common;

namespace DemoWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InvoiceViewModel InvoiceVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InvoiceVM = new InvoiceViewModel();
            this.DataContext = InvoiceVM;   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            setListBoxData(false);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            updateOrCreateInvoiceDetail();
        }

        private void listBoxInvoiceDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxInvoiceDetails.SelectedIndex != -1)
            {
                int index = ((ListBox)e.Source).SelectedIndex;
                InvoiceVM.SelectedInvoiceDetail = (InvoiceDetail)InvoiceVM.Invoice[index];
            }
        }

        private void updateListBox()
        {
            setListBoxData(true);
        }

        private void setListBoxData(bool clear)
        {
            if (clear)
            { 
                listBoxInvoiceDetails.ItemsSource = new Invoice();
            }
            ((Invoice)InvoiceVM.Invoice.DataSource).Sort();
            listBoxInvoiceDetails.ItemsSource = InvoiceVM.Invoice;
            listBoxInvoiceDetails.DisplayMemberPath = "Sku";
            listBoxInvoiceDetails.IsSynchronizedWithCurrentItem = true;
        }

        private void updateOrCreateInvoiceDetail()
        {
            if (InvoiceValidation.Validate(InvoiceVM.SelectedInvoiceDetail))
            {
                // here now need to decide whether we have a new or existing product
                int rowsAffected;

                // Update existing InvoiceDeail
                if(((Invoice)InvoiceVM.Invoice.DataSource).Any(e => e.DetailID == InvoiceVM.SelectedInvoiceDetail.DetailID))
                {
                    rowsAffected = InvoiceValidation.UpdateInvoice(InvoiceVM.SelectedInvoiceDetail, (Invoice)InvoiceVM.Invoice.DataSource);
                }
                // Add new InvoiceDetail
                else
                {
                    rowsAffected = 0;
                }
               
                if (rowsAffected < 0)
                {
                    MessageBox.Show("Other error occurred saving data", "Other error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    InvoiceVM.UpdateProperties();
                    updateListBox();
                }
            }
            else
            {
                MessageBox.Show(InvoiceValidation.GetErrorMessages(), "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
