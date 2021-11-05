using System.Windows;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        #region Constructors

        public CheckoutWindow()
        {
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

        #endregion
    }
}