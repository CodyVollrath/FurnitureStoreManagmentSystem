using FurnitureStoreManagmentSystem.ViewModel;
using System.Windows;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    /// <author>Cody Vollrath</author>
    public partial class EditCustomerWindow : Window
    {
        private EditCustomerViewModel editCustomerVM { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EditCustomerWindow" /> class.</summary>
        /// <param name="id">The identifier.</param>
        public EditCustomerWindow(int id)
        {
            this.editCustomerVM = new EditCustomerViewModel(id);
            InitializeComponent();
            DataContext = this.editCustomerVM;
        }

        private void EmptyError_Change(object sender, RoutedEventArgs e)
        {
            this.lblError.Text = "";
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            this.editCustomerVM.UpdateCustomer();
            
            this.lblError.Text = this.editCustomerVM.ErrorLabel;
            if (!this.editCustomerVM.HasError()) {
                this.GoBack();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            this.GoBack();
        }

        private void GoBack() 
        {
            var customerWindow = new CustomersWindow();
            customerWindow.Show();
            this.Close();
        }
    }
}
