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
    /// Interaction logic for AdminPortalWindow.xaml
    /// </summary>
    public partial class AdminPortalWindow : Window
    {
        private AdminPortalViewModel adminPortalVM;
        public AdminPortalWindow()
        {
            InitializeComponent();
            this.adminPortalVM = new AdminPortalViewModel();
            DataContext = adminPortalVM;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.adminPortalVM.SendCommand();
            this.txtOutput.Text = this.adminPortalVM.Output;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
