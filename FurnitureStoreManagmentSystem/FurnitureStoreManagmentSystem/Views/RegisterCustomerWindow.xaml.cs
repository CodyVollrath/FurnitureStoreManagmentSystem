using FurnitureStoreManagmentSystem.ViewModels;
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
    /// Interaction logic for RegisterCustomerWindow.xaml
    /// </summary>
    public partial class RegisterCustomerWindow : Window
    {
        private CustomerRegistrationViewModel customerRegisterVm { get; set; }
        public RegisterCustomerWindow()
        {
            InitializeComponent();
            this.DataContext = new CustomerRegistrationViewModel();
        }

        public void Register_Click(object sender, RoutedEventArgs e) 
        {
            var vm = (CustomerRegistrationViewModel)this.DataContext;
            vm.UploadCustomer();
            if (!vm.hasError())
            {
                this.goToMainAndClose();
            }
            else 
            {
                this.lblError.Content = vm.ErrorMessage;
            }
            
        }

        public void Close_Click(object sender, RoutedEventArgs e) 
        {
            this.goToMainAndClose();
        }

        private void goToMainAndClose() 
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
