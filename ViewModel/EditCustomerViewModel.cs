using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FurnitureStoreManagmentSystem.ViewModel
{

    /// <summary>The view model for the edit customer window</summary>
    /// <author>Cody Vollrath</author>
    public class EditCustomerViewModel
    {

        /// <summary>Gets the selected customer.</summary>
        /// <value>The selected customer.</value>
        public Customer SelectedCustomer { get; private set; }

        private CustomerDal customerDal { get; set; }

        /// <summary>Gets the genders.</summary>
        /// <value>The genders.</value>
        public List<string> Genders { get; private set; }

        /// <summary>Gets the states.</summary>
        /// <value>The states.</value>
        public List<string> States { get; private set; }


        /// <summary>Gets or sets the error label.</summary>
        /// <value>The error label.</value>
        public string ErrorLabel { get; set; } = "";


        /// <summary>Initializes a new instance of the <see cref="EditCustomerViewModel" /> class.</summary>
        /// <param name="id">The identifier.</param>
        public EditCustomerViewModel(int id) 
        {
            this.customerDal = new CustomerDal();
            this.GetCustomerById(id);
            this.Genders = Resources.Constants.Genders;
            this.States = Resources.Constants.States;
        }

        /// <summary>Updates the customer.</summary>
        public void UpdateCustomer()
        {
            this.ErrorLabel = this.SelectedCustomer.ValidateCustomer();
            if (!this.HasError()) 
            {
                this.customerDal.UpdateCustomer(this.SelectedCustomer);
            }
        }

        /// <summary>Gets the customer by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.NullReferenceException">Selected customer does not exist in the database</exception>
        private void GetCustomerById(int id) 
        {
            this.SelectedCustomer = this.customerDal.GetCustomersById(id).FirstOrDefault();
            if (this.SelectedCustomer == null)
            {
                throw new NullReferenceException("Selected customer does not exist in the database");
            }
        }

        /// <summary>Determines whether this instance has error.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.</returns>
        public bool HasError()
        {
            return this.ErrorLabel != "";
        }
    }
}
