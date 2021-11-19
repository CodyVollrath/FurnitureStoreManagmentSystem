using System.Windows;
using System.Windows.Controls;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.ViewModel;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for CustomersWindow.xaml
    /// </summary>
    /// <author>Cody Vollrath</author>
    public partial class CustomersWindow : Window
    {
        #region Properties

        private CustomersViewModel customerVM { get; }

        private FurnitureViewModel furnitureVM { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="CustomersWindow" /> class.</summary>
        public CustomersWindow()
        {
            this.customerVM = new CustomersViewModel();
            this.furnitureVM = new FurnitureViewModel();
            this.InitializeComponent();
            DataContext = this.customerVM;
            if (Singletons.CurrentTransaction == 0)
            {
                this.tIDText.Text = "No Current Transaction";
            }
            else
            {
                this.tIDText.Text = "Current Transaction ID: " + Singletons.CurrentTransaction;
            }
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the SearchCustomer control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.customerVM.ErrorLabel = "";
            this.customerVM.LoadSearchResults();
            if (this.customerVM.HasError())
            {
                this.lblError.Text = this.customerVM.ErrorLabel;
            }
        }

        /// <summary>Handles the Click event of the EditCustomer control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (this.customerVM.SelectedCustomer == null)
            {
                this.lblErrorSelectedCustomer.Text = "Customer is not Selected";
                return;
            }

            var editCustomerWindow = new EditCustomerWindow(this.customerVM.SelectedCustomer.Id);
            editCustomerWindow.Show();
            Close();
        }

        /// <summary>Handles the Click event of the Back control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        /// <summary>Handles the TextChanged event of the EmptyError control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs" /> instance containing the event data.</param>
        private void EmptyError_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblErrorSelectedCustomer.Text = "";
            this.lblError.Text = "";
        }

        /// <summary>Handles the SelectionChanged event of the lstResults control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void lstResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lblErrorSelectedCustomer.Text = "";
            this.lblError.Text = "";
        }

        public void CreateTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                this.furnitureVM.CreateTransaction(this.customerVM.SelectedCustomer.Id);
                Singletons.CurrentCustomer = (Customer) this.lstResults.SelectedItem;
                this.tIDText.Text = "Current Transaction ID: " + Singletons.CurrentTransaction;
            }
            else
            {
                this.lblErrorSelectedCustomer.Text = "Transaction failed: No customer selected";
            }
        }

        public void Return_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                Singletons.CurrentCustomer = (Customer) this.lstResults.SelectedItem;

                var returnWindow = new ReturnWindow();
                returnWindow.Show();
                Close();
            }
            else
            {
                this.lblErrorSelectedCustomer.Text = "Return failed: No customer selected";
            }
        }

        #endregion
    }
}