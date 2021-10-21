using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    public class LoginViewModel
    {
        public string ErrorMessage { get; set; } = "";
        public EmployeeDal EmployeeDal { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public LoginViewModel() 
        {
            this.EmployeeDal = new EmployeeDal();
        }

        public Employee AuthenticateEmployee() 
        {
            this.ErrorMessage = "";
            if (this.Username == "")
            {
                this.ErrorMessage = "Username is not entered";
                return null;
            }

            if (this.Password == "")
            {
                this.ErrorMessage = "Password is not entered";
                return null;
            }
            var employee = this.EmployeeDal.AuthenticateEmployee(this.Username, this.Password);

            if (employee == null) 
            {
                this.ErrorMessage = "Invalid Login Credentials: Check Username and Password";
                return employee;
            }
            return employee;
        }

        public bool hasError() 
        {
            return this.ErrorMessage != "";
        }

    }
}
