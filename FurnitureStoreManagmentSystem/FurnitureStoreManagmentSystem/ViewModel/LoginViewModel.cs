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
        public EmployeeDal EmployeeDal { get; set; }
        public string Username { get; set;}
        public string Password { get; set; }

        public LoginViewModel() 
        {
            this.EmployeeDal = new EmployeeDal();
        }

        public Employee AuthenticateEmployee() 
        {
            return this.EmployeeDal.AuthenticateEmployee(this.Username, this.Password);
        }
    }
}
