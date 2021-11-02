using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        CustomerDal customerDal { get; set; }
        private ObservableCollection<Customer> customerSearchResults;
        public ObservableCollection<Customer> CustomerSearchResults
        {
            get { return customerSearchResults; }
            set
            {
                customerSearchResults = value;
                this.OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer { get; set; }
        public string ErrorLabel { get; set; } = "";
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public CustomersViewModel()
        {
            this.customerDal = new CustomerDal();
        }
        public void validate_fields()
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
        public void LoadSearchResults()
        {
            int id;
            List<Customer> searchResults = new List<Customer>();
            this.validate_fields();
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

        public bool HasError() 
        {
            return this.ErrorLabel != "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
