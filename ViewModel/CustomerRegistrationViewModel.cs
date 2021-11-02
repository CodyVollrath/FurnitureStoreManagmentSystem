using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.ViewModels
{
    public class CustomerRegistrationViewModel
    {
        public CustomerDal customerDal { get; set; }
        public Customer CreatedCustomer { get; set; }
        public List<string> Genders { get; set; }
        public List<string> States { get; set; }
        public string ErrorMessage { get; set; } = "";
        public CustomerRegistrationViewModel()
        {
            this.customerDal = new CustomerDal();
            this.Genders = Resources.Constants.Genders;
            this.CreatedCustomer = new Customer();
            this.States = Resources.Constants.States;
        }

        public void UploadCustomer() 
        {
            this.ErrorMessage = this.CreatedCustomer.ValidateCustomer();
            if (this.hasError()) 
            {
                return;
            }
            try
            {
                this.customerDal.CreateCustomer(this.CreatedCustomer);
            }
            catch (Exception e) 
            {
                this.ErrorMessage = e.Message;
            }
            
        }

        public bool hasError() 
        {
            return this.ErrorMessage != "";
        }

    }
}
