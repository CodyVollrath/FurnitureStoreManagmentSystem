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

        public CheckoutWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            this.InitializeComponent();
            this.customerText.Text = "Customer: " + Singletons.CurrentCustomer.FirstName + " " +
                                     Singletons.CurrentCustomer.LastName;
            this.transactionText.Text = "Transaction ID: " + Singletons.CurrentTransaction;
            this.priceText.Text = "TODO";
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
                this.furnitureVM.ModifyFurnitureQuantity(furniture.Id, furniture.Quantity);
            }

            var furnitureWindow = new FurnitureWindow();
            furnitureWindow.Show();
            Singletons.CurrentTransaction = 0;
            Singletons.CurrentCustomer = null;
            Singletons.FurnitureCart = new List<Furniture>();
            Close();
        }

        #endregion
    }
}