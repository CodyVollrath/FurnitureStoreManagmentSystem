using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.ViewModel
{
     
    public class EditCustomerViewModel
    {
        public Customer SelectedCustomer { get; private set; }
        private CustomerDal customerDal { get; set; }

        public List<string> Genders { get; private set; }
        public List<string> States { get; private set; }
        
        public EditCustomerViewModel(int id) 
        {
            this.customerDal = new CustomerDal();
            this.GetCustomerById(id);
            this.Genders = Resources.Constants.Genders;
            this.States = Resources.Constants.States;
        }

        public void UpdateCustomer() 
        {
            this.customerDal.UpdateCustomer(this.SelectedCustomer);
            int id = this.SelectedCustomer.Id;
            this.GetCustomerById(id);
        }

        private void GetCustomerById(int id) 
        {
            this.SelectedCustomer = this.customerDal.GetCustomersById(id).FirstOrDefault();
            if (this.SelectedCustomer == null)
            {
                throw new NullReferenceException("Selected customer does not exist in the database");
            }
        }
    }
}
