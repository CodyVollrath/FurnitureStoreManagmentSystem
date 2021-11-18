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
        public AdminPortalWindow()
        {
            InitializeComponent();
        }

        private void btnSql_Click(object sender, RoutedEventArgs e)
        {
            var terminal = new AdminTerminalWindow();
            terminal.Show();
        }
    }
}
