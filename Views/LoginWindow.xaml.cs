using FurnitureStoreManagmentSystem.ViewModel;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            //Add Functionality to validate login
            var vm = (LoginViewModel)this.DataContext;
            vm.Password = txtPassword.Password;
            var employee = vm.AuthenticateEmployee();
            if (!vm.hasError())
            {
                var mvm = new MainViewModel();
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

        public void UsernameField_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }

        public void PasswordField_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Content = "";
        }
    }
}
