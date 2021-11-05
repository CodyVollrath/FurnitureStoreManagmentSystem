using System.Windows;

namespace FurnitureStoreManagmentSystem.Views
{
    /// <summary>
    ///     Interaction logic for QuantityDialog.xaml
    /// </summary>
    public partial class QuantityDialog : Window
    {
        #region Properties

        public int Quantity { get; set; }

        private int CurrQuantity { get; }

        public string Answer => this.txtAnswer.Text;

        #endregion

        #region Constructors

        public QuantityDialog(int currQuantity)
        {
            this.InitializeComponent();
            this.CurrQuantity = currQuantity;
            this.lblQuestion.Text = "Enter a # to be Added:  Current # Available is " + this.CurrQuantity;
        }

        #endregion

        #region Methods

        /// <summary>Handles the Click event of the Ok control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Quantity = int.Parse(this.Answer);
                if (this.Quantity > this.CurrQuantity)
                {
                    this.errorText.Text = "The entered # is more than the available quantity.";
                }
                else if (this.Quantity <= 0)
                {
                    this.errorText.Text = "Please enter a quantity greater then 0.";
                }
                else
                {
                    Singletons.Quantity = this.Quantity;
                    Close();
                }
            }
            catch
            {
                this.errorText.Text = "Please enter a valid number";
            }
        }

        /// <summary>Handles the Click event of the Cancel control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Quantity = 0;
            Close();
        }

        #endregion
    }
}