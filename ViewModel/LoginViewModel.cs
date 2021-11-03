using FurnitureStoreManagmentSystem.DAL;
using FurnitureStoreManagmentSystem.Models;

namespace FurnitureStoreManagmentSystem.ViewModel
{

    /// <summary>The view model for the login window</summary>
    /// <author>Cody Vollrath</author>
    public class LoginViewModel
    {

        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; } = "";


        /// <summary>Gets or sets the employee dal.</summary>
        /// <value>The employee dal.</value>
        public EmployeeDal EmployeeDal { get; set; }


        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public string Username { get; set; } = "";


        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; } = "";


        /// <summary>Initializes a new instance of the <see cref="LoginViewModel" /> class.</summary>
        public LoginViewModel() 
        {
            this.EmployeeDal = new EmployeeDal();
        }

        /// <summary>Authenticates the employee.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
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


        /// <summary>Determines whether this instance has error.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.</returns>
        public bool HasError() 
        {
            return this.ErrorMessage != "";
        }

    }
}
