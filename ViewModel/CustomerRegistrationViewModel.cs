using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;

namespace FurnitureStoreManagmentSystem.ViewModels
{

    /// <summary>The view model for the Customer registration window</summary>
    /// <author>Cody Vollrath</author>
    public class CustomerRegistrationViewModel
    {

        private CustomerDal customerDal { get; set; }

        /// <summary>Gets or sets the created customer.</summary>
        /// <value>The created customer.</value>
        public Customer CreatedCustomer { get; set; }

        /// <summary>Gets the genders.</summary>
        /// <value>The genders.</value>
        public List<string> Genders { get; private set; }

        /// <summary>Gets the states.</summary>
        /// <value>The states.</value>
        public List<string> States { get; private set; }

        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; } = "";
        public CustomerRegistrationViewModel()
        {
            this.customerDal = new CustomerDal();
            this.Genders = Resources.Constants.Genders;
            this.CreatedCustomer = new Customer();
            this.States = Resources.Constants.States;
        }


        /// <summary>Uploads the customer.</summary>
        public void UploadCustomer() 
        {
            this.ErrorMessage = this.CreatedCustomer.ValidateCustomer();
            if (this.HasError()) 
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

        /// <summary>Determines whether this instance has errors.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.</returns>
        public bool HasError() 
        {
            return this.ErrorMessage != "";
        }

    }
}
