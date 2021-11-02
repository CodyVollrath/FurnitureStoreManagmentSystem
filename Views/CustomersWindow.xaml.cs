using FurnitureStoreManagmentSystem.ViewModel;
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
using System.Windows.Shapes;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    /// Interaction logic for CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        private CustomersViewModel customerVM { get; set; }

        public CustomersWindow()
        {
            this.customerVM = new CustomersViewModel();
            InitializeComponent();
            DataContext = this.customerVM;
            
        }

        public void SearchCustomer_Click(object sender, RoutedEventArgs e) 
        {
            this.customerVM.LoadSearchResults();
        }

        public void EditCustomer_Click(object sender, RoutedEventArgs e) 
        {
            if (this.customerVM.SelectedCustomer == null) 
            {
                return;
            }

            var editCustomerWindow = new EditCustomerWindow(this.customerVM.SelectedCustomer.Id);
            editCustomerWindow.Show();
            this.Close();
        }

        public void Back_Click(object sender, RoutedEventArgs e) 
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
