using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FurnitureStoreManagmentSystem.Extensions
{

    /// <summary>Handles Extensions for Customers</summary>
    /// <author>Cody Vollrath</author>
    public static class CustomerExtensions
    {

        /// <summary>Converts an Enumerable collection of customers to an observable collection.</summary>
        /// <param name="customers">The customers - the collection to be converted to an observable list.</param>
        /// <returns>The observable collection of customers</returns>
        public static ObservableCollection<Customer> ConvertToObservable(this IEnumerable<Customer> customers) 
        {
            var observableCustomers = new ObservableCollection<Customer>(customers);
            return observableCustomers;
        }


        /// <summary>Validates the customer fields.</summary>
        /// <param name="customer">The customer - the customer to be validated.</param>
        /// <returns>A string that indicates the field violation; Empty if no violation occurs</returns>
        public static string ValidateCustomer(this Customer customer)
        {
            if (customer is null)
            {
                return "Customer data has not been entered";
            }
            if (customer.FirstName == "")
            {
                return "Customer First Name has not been entered";
            }
            if (customer.LastName == "")
            {
                return "Customer Last Name has not been entered";
            }
            if (customer.Gender == "")
            {
                return "Customer Gender has not been listed";
            }
            if (customer.BirthDay.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
            {
                return "Customer birthday is invalid";
            }

            if (customer.PhoneNumber == "" || customer.PhoneNumber.Length != 10)
            {
                return "Customer Phone Number is invalid";
            }
            if (customer.Address1 == "")
            {
                return "Customer must have a primary address";
            }
            if (customer.State == "")
            {
                return "Customer must live in a state";
            }
            if (customer.City == "")
            {
                return "Customer must live in a city";
            }

            if (customer.ZipCode == "" || customer.ZipCode.Length != 5)
            {
                return "Customer must have a valid zip-code";
            }
            return "";
        }
    }
}
