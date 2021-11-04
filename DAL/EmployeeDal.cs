using System.Collections.Generic;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;

namespace FurnitureStoreManagmentSystem.DAL
{
    /// <summary>Performs SQL operations on the Employee table</summary>
    /// <author>Cody Vollrath</author>
    /// <version>Fall 2021</version>
    public class EmployeeDal
    {
        #region Methods

        /// <summary>Gets the employees from the database.</summary>
        /// <returns>Employees</returns>
        public List<Employee> GetEmployees()
        {
            var employeeList = new List<Employee>();
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select * from employee";
                using var command = new MySqlCommand(query, connection);

                using var reader = command.ExecuteReader();
                var eId = reader.GetOrdinal("eID");
                var username = reader.GetOrdinal("username");
                var password = reader.GetOrdinal("password");
                var firstName = reader.GetOrdinal("firstName");
                var lastName = reader.GetOrdinal("lastName");

                while (reader.Read())
                {
                    employeeList.Add(new Employee
                    {
                        FirstName = reader.GetFieldValueCheckNull<string>(firstName),
                        Lastname = reader.GetFieldValueCheckNull<string>(lastName),
                        Id = reader.GetFieldValueCheckNull<int>(eId),
                        Username = reader.GetFieldValueCheckNull<string>(username),
                        Password = reader.GetFieldValueCheckNull<string>(password)
                    });
                }
            }

            return employeeList;
        }

        /// <summary>Authenticates the employee.</summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The employee that was authenticated</returns>
        public Employee AuthenticateEmployee(string username, string password)
        {
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query =
                    "select username, password, eID, firstName, lastName from employee where username = @username and password = @password";

                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                using var reader = command.ExecuteReader();
                var eIDOrdinal = reader.GetOrdinal("eID");
                var usernameOrdinal = reader.GetOrdinal("username");
                var firstNameOrdinal = reader.GetOrdinal("firstName");
                var lastNameOrdinal = reader.GetOrdinal("lastName");
                Employee employee = null;
                while (reader.Read())
                {
                    employee = new Employee
                    {
                        Id = reader.GetFieldValueCheckNull<int>(eIDOrdinal),
                        FirstName = reader.GetFieldValueCheckNull<string>(firstNameOrdinal),
                        Lastname = reader.GetFieldValueCheckNull<string>(lastNameOrdinal),
                        Username = reader.GetFieldValueCheckNull<string>(usernameOrdinal)
                    };
                    break;
                }

                return employee;
            }
        }

        #endregion
    }
}