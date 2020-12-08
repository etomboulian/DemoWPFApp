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
using DemoWFPApp.Business;
using DemoWFPApp.Common;

namespace DemoWFPApp
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
            listBoxInvoiceDetails.ItemsSource = InvoiceVM.Invoice;
            listBoxInvoiceDetails.DisplayMemberPath = "Sku";
            listBoxInvoiceDetails.IsSynchronizedWithCurrentItem = true;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if(InvoiceValidation.Validate(InvoiceVM.SelectedInvoiceDetail))
            {
                int rowsAffected;
                rowsAffected = InvoiceValidation.UpdateInvoice(InvoiceVM.SelectedInvoiceDetail, (Invoice)InvoiceVM.Invoice.DataSource);

                if(rowsAffected < 0)
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
            listBoxInvoiceDetails.ItemsSource = new Invoice();
            ((Invoice)InvoiceVM.Invoice.DataSource).Sort();
            listBoxInvoiceDetails.ItemsSource = InvoiceVM.Invoice;
            listBoxInvoiceDetails.DisplayMemberPath = "Sku";
            listBoxInvoiceDetails.IsSynchronizedWithCurrentItem = true;
        }

   
    }
}
