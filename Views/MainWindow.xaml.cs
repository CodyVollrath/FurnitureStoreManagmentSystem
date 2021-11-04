using System.Windows;
using FurnitureStoreManagmentSystem.Views;

namespace FurnitureStoreManagmentSystem
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    /// <action>Cody Vollrath and Daniel Crumpler</action>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="MainWindow" /> class.</summary>
        public MainWindow()
        {
            this.InitializeComponent();
            var fullName = $"{Singletons.CurrentEmployee.FirstName} {Singletons.CurrentEmployee.Lastname}";
            this.lblUserInfo.Content = string.Format(this.lblUserInfo.Content.ToString(), Singletons.CurrentEmployee.Id,
                Singletons.CurrentEmployee.Username, fullName);
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the RegisterCustomer control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            var registerCustomer = new RegisterCustomerWindow();
            registerCustomer.Show();
            Close();
        }

        /// <summary>Handles the Click event of the Customers control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Customers_Click(object sender, RoutedEventArgs e)
        {
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            Close();
        }

        /// <summary>Handles the Click event of the LogOut control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            Singletons.CurrentEmployee = null;
            loginWindow.Show();
            Close();
        }

        /// <summary>Handles the Click event of the LogOut control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Furniture_Click(object sender, RoutedEventArgs e)
        {
            var furnitureWindow = new FurnitureWindow();
            furnitureWindow.Show();
            Close();
        }

        #endregion
    }
}