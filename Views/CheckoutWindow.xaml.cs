using System.Collections.Generic;
using System.Windows;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.ViewModel;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        #region Properties

        private FurnitureViewModel furnitureVM { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CheckoutWindow" /> class.</summary>
        public CheckoutWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            this.InitializeComponent();
            DataContext = this.furnitureVM;
            this.customerText.Text = "Customer: " + Singletons.CurrentCustomer.FirstName + " " +
                                     Singletons.CurrentCustomer.LastName;
            this.transactionText.Text = "Transaction ID: " + Singletons.CurrentTransaction;
            double totalCost = 0;
            foreach (var furniture in Singletons.FurnitureCart)
            {
                totalCost += furniture.Price * furniture.Quantity;
            }

            Singletons.TotalCost = totalCost;
            this.priceText.Text = "Total Cost: " + totalCost.ToString("C");
            this.furnitureVM.LoadCart();
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the Back control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            var furnitureWindow = new FurnitureWindow();
            furnitureWindow.Show();
            Close();
        }

        /// <summary>Handles the Click event of the Checkout control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Checkout_Click(object sender, RoutedEventArgs e)
        {
            foreach (var furniture in Singletons.FurnitureCart)
            {
                this.furnitureVM.CreateItemCheckOut(furniture.Id, Singletons.CurrentTransaction, furniture.Quantity);
            }

            this.furnitureVM.CreateRental(Singletons.CurrentTransaction, Singletons.TotalCost);
            var furnitureWindow = new FurnitureWindow();
            furnitureWindow.Show();
            Singletons.CurrentTransaction = 0;
            Singletons.CurrentCustomer = null;
            Singletons.FurnitureCart = new List<Furniture>();
            Close();
        }

        /// <summary>Handles the Click event of the remove control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                Singletons.FurnitureCart.Remove((Furniture) this.lstResults.SelectedItem);
                this.lstResults.ItemsSource = this.furnitureVM.LoadCart();
                double totalCost = 0;
                foreach (var furniture in Singletons.FurnitureCart)
                {
                    totalCost += furniture.Price * furniture.Quantity;
                }

                Singletons.TotalCost = totalCost;
                this.priceText.Text = "Total Cost: " + totalCost.ToString("C");
            }
        }

        #endregion
    }
}