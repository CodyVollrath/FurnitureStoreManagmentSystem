﻿using System.Threading.Tasks;
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

        /// <summary>Initializes a new instance of the <see cref="FurnitureWindow" /> class.</summary>
        public FurnitureWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            this.InitializeComponent();
            DataContext = this.furnitureVM;
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
            var furnitureVm = this.furnitureVM;
            var searchDialog = new FurnitureSearchFilterDialog(ref furnitureVm);
            searchDialog.ShowDialog();
            this.lstResults.ItemsSource = furnitureVm.FurnitureSearchResults;
        }

        /// <summary>Handles the Click event of the Add Cart control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public async void AddCart_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                var selectedFurniture = (Furniture) this.lstResults.SelectedItem;
                var containsFurniture = false;

                foreach (var furn in Singletons.FurnitureCart)
                {
                    if (furn.Id == selectedFurniture.Id)
                    {
                        containsFurniture = true;
                    }
                }

                if (selectedFurniture.Quantity == 0)
                {
                    this.lblError.Text = "This item is out of stock";
                    this.lblError.Focus();
                }
                else if (!containsFurniture)
                {
                    var furniture = new Furniture
                    {
                        Id = selectedFurniture.Id,
                        CategoryName = selectedFurniture.CategoryName,
                        ItemDescription = selectedFurniture.ItemDescription,
                        ItemName = selectedFurniture.ItemName,
                        Quantity = selectedFurniture.Quantity,
                        StyleName = selectedFurniture.StyleName,
                        Price = selectedFurniture.Price
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
                    this.lblError.Focus();
                }
            }
            else
            {
                this.lblError.Text = "An item must be selected to add to the cart";
                this.lblError.Focus();
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
                this.lblError.Focus();
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

        private void Error_LostFocus(object sender, RoutedEventArgs e)
        {
            this.lblError.Text = "";
        }

        #endregion
    }
}