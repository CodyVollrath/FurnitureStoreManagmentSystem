using FurnitureStoreManagmentSystem.ViewModels;
using System.Windows;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    /// Interaction logic for RegisterCustomerWindow.xaml
    /// </summary>
    /// <author>Cody Vollrath</author>
    public partial class RegisterCustomerWindow : Window
    {
        private CustomerRegistrationViewModel customerRegisterVm { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RegisterCustomerWindow" /> class.</summary>
        public RegisterCustomerWindow()
        {
            InitializeComponent();
            this.DataContext = new CustomerRegistrationViewModel();
        }

        /// <summary>Handles the Click event of the Register control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Register_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CustomerRegistrationViewModel)this.DataContext;
            vm.UploadCustomer();
            if (!vm.HasError())
            {
                this.goToMainAndClose();
            }
            else
            {
                this.lblError.Content = vm.ErrorMessage;
            }

        }

        /// <summary>Handles the Click event of the Close control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Close_Click(object sender, RoutedEventArgs e)
        {
            this.goToMainAndClose();
        }

        /// <summary>Handles the Change event of the EmptyError control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void EmptyError_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }

        /// <summary>Goes to main and close.</summary>
        private void goToMainAndClose()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
