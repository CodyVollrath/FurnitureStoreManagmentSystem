using FurnitureStoreManagmentSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for FurnitureWindow.xaml
    /// </summary>
    public partial class FurnitureWindow : Window
    {
        private FurnitureViewModel furnitureVM { get; set; }

        public FurnitureWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            InitializeComponent();
            DataContext = this.furnitureVM;
            this.furnitureVM.LoadSearchResults(this.searchBox.Text);
        }

        /// <summary>Handles the Click event of the Back control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            this.lstResults.ItemsSource = this.furnitureVM.LoadSearchResults(this.searchBox.Text);
        }
    }
}
