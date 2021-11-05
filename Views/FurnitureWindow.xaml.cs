using System.Threading.Tasks;
using System.Windows;
using FurnitureStoreManagmentSystem.Models;
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

        /// <summary>Handles the Click event of the Search control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Search_Click(object sender, RoutedEventArgs e)
        {
            this.lstResults.ItemsSource = this.furnitureVM.LoadSearchResults(this.searchBox.Text);
        }

        /// <summary>Handles the Click event of the Add Cart control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public async void AddCart_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                var selectedFurniture = (Furniture)this.lstResults.SelectedItem;
                var containsFurniture = false;
                foreach (Furniture furn in Singletons.FurnitureCart)
                {
                    if (furn.Id == selectedFurniture.Id)
                    {
                        containsFurniture = true;
                    }
                }
                if (!containsFurniture)
                {
                    var furniture = new Furniture
                    {
                        Id = selectedFurniture.Id,
                        CategoryName = selectedFurniture.CategoryName,
                        ItemDescription = selectedFurniture.ItemDescription,
                        ItemName = selectedFurniture.ItemName,
                        Quantity = selectedFurniture.Quantity,
                        StyleName = selectedFurniture.StyleName
                    };
                    await this.ShowPopup(new QuantityDialog(furniture.Quantity));
                    furniture.Quantity = Singletons.Quantity;
                    if (furniture.Quantity > 0)
                    {
                        Singletons.FurnitureCart.Add(furniture);
                    }
                }
                else
                {
                    this.lblError.Text = "This item is already in the cart";
                }
            }
            else
            {
                this.lblError.Text = "An item must be selected to add to the cart";
            }
        }

        /// <summary>Handles the Click event of the Checkout control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (Singletons.CurrentCustomer != null)
            {
                var checkoutWindow = new CheckoutWindow();
                checkoutWindow.Show();
                Close();
            }
            else
            {
                this.lblError.Text = "Please start a transaction";
            }
        }

        private Task ShowPopup<TPopup>(TPopup popup)
            where TPopup : Window
        {
            var task = new TaskCompletionSource<object>();
            popup.Owner = Application.Current.MainWindow;
            popup.Closed += (s, a) => task.SetResult(null);
            popup.Show();
            popup.Focus();
            return task.Task;
        }

        #endregion
    }
}