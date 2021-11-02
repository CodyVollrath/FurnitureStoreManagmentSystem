using FurnitureStoreManagmentSystem.ViewModels;
using FurnitureStoreManagmentSystem.Views;
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

namespace FurnitureStoreManagmentSystem
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            var fullName = $"{Singletons.CurrentEmployee.FirstName} {Singletons.CurrentEmployee.Lastname}";
            this.lblUserInfo.Content = String.Format(this.lblUserInfo.Content.ToString(), Singletons.CurrentEmployee.eId, Singletons.CurrentEmployee.Username, fullName);

        }

        public void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            var registerCustomer = new RegisterCustomerWindow();
            registerCustomer.Show();
            this.Close();
        }

        public void Customers_Click(object sender, RoutedEventArgs e) 
        {
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            this.Close();
        }

        public void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            Singletons.CurrentEmployee = null;
            loginWindow.Show();
            this.Close();
        }
    }
}
