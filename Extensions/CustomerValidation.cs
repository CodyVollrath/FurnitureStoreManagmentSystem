using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.Extensions
{
    public static class CustomerValidation
    {
        public static ObservableCollection<Customer> ConvertToObservable(this IEnumerable<Customer> customers) 
        {
            var observableCustomers = new ObservableCollection<Customer>(customers);
            return observableCustomers;
        }

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
