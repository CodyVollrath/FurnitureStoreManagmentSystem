using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.ViewModel;
using Org.BouncyCastle.Asn1.X9;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        #region Data members

        private readonly List<int> transactionComboList;

        #endregion

        #region Properties

        private FurnitureViewModel furnitureVM { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ReturnWindow" /> class.</summary>
        public ReturnWindow()
        {
            this.furnitureVM = new FurnitureViewModel();
            this.transactionComboList = new List<int>();
            this.InitializeComponent();
            DataContext = this.furnitureVM;
            this.customerText.Text = "Customer: " + Singletons.CurrentCustomer.FirstName + " " +
                                     Singletons.CurrentCustomer.LastName;
            this.transactionComboList = this.furnitureVM.ReturnFurniture(Singletons.CurrentCustomer.Id);
            this.transactionCombo.ItemsSource = this.transactionComboList;
            this.furnitureVM.LoadCart();
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the Back control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Back_Click(object sender, RoutedEventArgs e)
        {
            Singletons.CurrentTransaction = 0;
            Singletons.CurrentCustomer = null;
            Singletons.FurnitureCart = new List<Furniture>();
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            Close();
        }

        /// <summary>Handles the Click event of the Return control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Return_Click(object sender, RoutedEventArgs e)
        {
            this.furnitureVM.CreateTransaction(Singletons.CurrentCustomer.Id);
            double fees = 0;
            foreach (var furn in Singletons.FurnitureCart)
            {
                this.furnitureVM.ReturnItems(furn.Id, furn.tID, furn.Quantity, Singletons.CurrentTransaction);
                if (this.furnitureVM.DetermineLateFees(furn.tID) > 0)
                {
                    fees += furn.Price * this.furnitureVM.DetermineLateFees(furn.tID);
                }
            }

            this.furnitureVM.CreateReturn(Singletons.CurrentTransaction, fees);
            this.backButton.Content = "Close";
            this.returnButton.Content = "Return Successful";
            this.returnButton.IsEnabled = false;
            this.addButton.IsEnabled = false;
            this.transactionCombo.IsEnabled = false;
            this.lstResults.ItemsSource = Singletons.FurnitureCart.ConvertToObservable();
            this.lblError.Text = "Fees: " + fees.ToString("C");
        }

        /// <summary>Handles the Click event of the Add control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstResults.SelectedItem != null)
            {
                var selectedFurniture = (Furniture) this.lstResults.SelectedItem;
                var containsFurniture = false;

                foreach (var furn in Singletons.FurnitureCart)
                {
                    if (furn.Id == selectedFurniture.Id &&
                        furn.tID == this.transactionComboList[this.transactionCombo.SelectedIndex])
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
                        StyleName = selectedFurniture.StyleName,
                        Price = selectedFurniture.Price,
                        tID = this.transactionComboList[this.transactionCombo.SelectedIndex]
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
                    this.lblError.Text = "This item is already in the return cart";
                    this.lblError.Focus();
                }
            }
            else
            {
                this.lblError.Text = "An item must be selected to add to the return cart";
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lstResults.ItemsSource =
                this.furnitureVM.GetFurnitureInRentals(this.transactionComboList[this.transactionCombo.SelectedIndex])
                    .ConvertToObservable();
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lblError.Text = "";
        }

        #endregion
    }
}