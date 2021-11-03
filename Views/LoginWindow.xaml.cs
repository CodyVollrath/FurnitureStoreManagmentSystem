using FurnitureStoreManagmentSystem.ViewModel;
using System.Windows;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    /// <author>Cody Vollrath</author>
    public partial class LoginWindow : Window
    {

        /// <summary>Initializes a new instance of the <see cref="LoginWindow" /> class.</summary>
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }


        /// <summary>Handles the Click event of the Login control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Login_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)this.DataContext;
            vm.Password = txtPassword.Password;
            var employee = vm.AuthenticateEmployee();
            if (!vm.HasError())
            {
                Singletons.CurrentEmployee = employee;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                this.lblError.Content = vm.ErrorMessage;
            }
        }


        /// <summary>Handles the Change event of the UsernameField control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void UsernameField_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }

        /// <summary>Handles the Change event of the PasswordField control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void PasswordField_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }
    }
}
