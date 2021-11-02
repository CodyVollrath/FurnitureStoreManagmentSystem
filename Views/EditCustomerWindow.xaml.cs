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
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        private EditCustomerViewModel editCustomerVM { get; set; }
        public EditCustomerWindow(int id)
        {
            this.editCustomerVM = new EditCustomerViewModel(id);
            InitializeComponent();
            DataContext = this.editCustomerVM;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            this.editCustomerVM.UpdateCustomer();
            this.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            this.GoBack();
        }

        private void GoBack() 
        {
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            this.Close();
        }
    }
}
