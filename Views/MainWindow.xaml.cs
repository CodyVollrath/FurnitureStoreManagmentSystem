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
    /// <action>Cody Vollrath</action>
    public partial class MainWindow : Window
    {

        /// <summary>Initializes a new instance of the <see cref="MainWindow" /> class.</summary>
        public MainWindow()
        {
            InitializeComponent();
            var fullName = $"{Singletons.CurrentEmployee.FirstName} {Singletons.CurrentEmployee.Lastname}";
            this.lblUserInfo.Content = String.Format(this.lblUserInfo.Content.ToString(), Singletons.CurrentEmployee.Id, Singletons.CurrentEmployee.Username, fullName);

        }


        /// <summary>Handles the Click event of the RegisterCustomer control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            var registerCustomer = new RegisterCustomerWindow();
            registerCustomer.Show();
            this.Close();
        }


        /// <summary>Handles the Click event of the Customers control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Customers_Click(object sender, RoutedEventArgs e) 
        {
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            this.Close();
        }


        /// <summary>Handles the Click event of the LogOut control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            Singletons.CurrentEmployee = null;
            loginWindow.Show();
            this.Close();
        }
    }
}
