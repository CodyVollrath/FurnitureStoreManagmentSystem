using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FurnitureStoreManagmentSystem.DAL
{

    /// <summary>Performs SQL operations on the Employee table</summary>
    /// <author>Cody Vollrath</author>
    /// <version>Fall 2021</version>
    public class EmployeeDal
    {

        /// <summary>Gets the employees from the database.</summary>
        /// <returns>Employees</returns>
        public List<Employee> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select * from employee";
                using MySqlCommand command = new MySqlCommand(query, connection);

                using MySqlDataReader reader = command.ExecuteReader();
                int eId = reader.GetOrdinal("eID");
                int username = reader.GetOrdinal("username");
                int password = reader.GetOrdinal("password");
                int firstName = reader.GetOrdinal("firstName");
                int lastName = reader.GetOrdinal("lastName");

                while (reader.Read())
                {
                    employeeList.Add(new Employee {
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
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString)) 
            {
                connection.Open();
                string query = "select username, password, eID, firstName, lastName from employee where username = @username and password = @password";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                using MySqlDataReader reader = command.ExecuteReader();
                int eIDOrdinal = reader.GetOrdinal("eID");
                int usernameOrdinal = reader.GetOrdinal("username");
                int firstNameOrdinal = reader.GetOrdinal("firstName");
                int lastNameOrdinal = reader.GetOrdinal("lastName");
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
    }
}
