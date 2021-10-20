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
            this.Genders = new List<string> { "Male", "Female"};
            this.CreatedCustomer = new Customer();
            this.States = new List<string> {
            
                "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", 
                "District of Columbia", "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", 
                "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", 
                "Minor Outlying Islands", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", 
                "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands", 
                "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", 
                "Tennessee", "Texas", "U.S. Virgin Islands", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
            };
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
