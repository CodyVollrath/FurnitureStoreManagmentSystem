using FurnitureStoreManagmentSystem.ViewModels;
using System.Windows;

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

        public void EmptyError_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }

        private void goToMainAndClose()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
