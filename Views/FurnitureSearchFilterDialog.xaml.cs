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
    /// Interaction logic for FurnitureSearchFilterDialog.xaml
    /// </summary>
    public partial class FurnitureSearchFilterDialog : Window
    {
        private FurnitureViewModel furnitureVm { get; set; }
        public FurnitureSearchFilterDialog(ref FurnitureViewModel furnitureVm)
        {
            InitializeComponent();
            this.furnitureVm = furnitureVm;
            DataContext = furnitureVm;
        }

        private void btnSearh_Click(object sender, RoutedEventArgs e)
        {
            //Perform search results
            this.furnitureVm.LoadSearchResults();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
