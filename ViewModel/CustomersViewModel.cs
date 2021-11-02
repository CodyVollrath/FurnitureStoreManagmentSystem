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

        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public CustomersViewModel() 
        {
            this.customerDal = new CustomerDal();
        }

        public void LoadSearchResults() 
        {
            int id;
            List<Customer> searchResults = new List<Customer>(); 

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

            this.CustomerSearchResults = searchResults.ConvertToObservable();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
