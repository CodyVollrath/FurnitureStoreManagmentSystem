using System.Windows;
using FurnitureStoreManagmentSystem.ViewModel;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for FurnitureWindow.xaml
    /// </summary>
    public partial class FurnitureWindow : Window
    {
        #region Properties

        private FurnitureViewModel furnitureVM { get; }

        #endregion

        #region Constructors

        public FurnitureWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            this.InitializeComponent();
            DataContext = this.furnitureVM;
            this.furnitureVM.LoadSearchResults(this.searchBox.Text);
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the Back control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            this.lstResults.ItemsSource = this.furnitureVM.LoadSearchResults(this.searchBox.Text);
        }

        #endregion
    }
}