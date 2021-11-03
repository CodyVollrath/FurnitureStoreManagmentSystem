using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FurnitureStoreManagmentSystem.ViewModel
{

    /// <summary>The View Model for the Customers Window</summary>
    /// <author>Cody Vollrath</author>
    public class CustomersViewModel : INotifyPropertyChanged
    {
        private CustomerDal customerDal { get; set; }
        private ObservableCollection<Customer> customerSearchResults;

        /// <summary>Gets or sets the customer search results.</summary>
        /// <value>The customer search results.</value>
        public ObservableCollection<Customer> CustomerSearchResults
        {
            get { return customerSearchResults; }
            set
            {
                customerSearchResults = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the selected customer.</summary>
        /// <value>The selected customer.</value>
        public Customer SelectedCustomer { get; set; }

        /// <summary>Gets or sets the error label.</summary>
        /// <value>The error label.</value>
        public string ErrorLabel { get; set; } = "";

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>Gets or sets the full name.</summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CustomersViewModel" /> class.</summary>
        public CustomersViewModel()
        {
            this.customerDal = new CustomerDal();
        }

        /// <summary>Validates the fields.</summary>
        public void Validate_fields()
        {
            Regex idRegex = new Regex(@"^\d*$");
            Regex fullNameRegex = new Regex(@"^\w+ \w+$");
            Regex phoneNumberRegex = new Regex(@"^\d{10}$");

            if (this.Id != null && this.Id != "" && !idRegex.IsMatch(this.Id))
            {
                this.ErrorLabel = "Invalid ID Entered";
            }

            if (this.FullName != null && this.FullName != "" && !fullNameRegex.IsMatch(this.FullName)) 
            {
                this.ErrorLabel = "Invalid Full Name Entered";
            }

            if (this.PhoneNumber != null && this.PhoneNumber != "" && !phoneNumberRegex.IsMatch(this.PhoneNumber)) 
            {
                this.ErrorLabel = "Invalid Phone Number Entered";
            }

        }

        /// <summary>Loads the search results.</summary>
        public void LoadSearchResults()
        {
            int id;
            List<Customer> searchResults = new List<Customer>();
            this.Validate_fields();
            if (!this.HasError()) 
            {
                if (this.Id != null && this.Id != "")
                {
                    id = int.Parse(this.Id);
                    var results = this.customerDal.GetCustomersById(id);
                    searchResults = searchResults.Concat(results).ToList();
                }

                if (this.FullName != null && this.FullName != "")
                {
                    var results = this.customerDal.GetCustomerByFullname(this.FullName);
                    searchResults = searchResults.Concat(results).ToList();
                }

                if (this.PhoneNumber != null && this.PhoneNumber != "")
                {
                    var results = this.customerDal.GetCustomersByPhoneNumber(this.PhoneNumber);
                    searchResults = searchResults.Concat(results).ToList();
                }
            }
            this.CustomerSearchResults = searchResults.ConvertToObservable();
        }

        /// <summary>Determines whether this instance has error.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.</returns>
        public bool HasError() 
        {
            return this.ErrorLabel != "";
        }

        /// <summary>Occurs when [property changed].</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
