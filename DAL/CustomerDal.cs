using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FurnitureStoreManagmentSystem.DAL
{

    /// <summary>Performs SQL operations on the Customer table</summary>
    /// <author>Cody Vollrath</author>
    /// <version>Fall 2021</version>
    public class CustomerDal
    {

        /// <summary>Creates the customer in the database.</summary>
        /// <param name="customer">The customer.</param>
        public void CreateCustomer(Customer customer)
        {
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "insert into customer (firstName, lastName, gender, dob, registrationDate, phoneNumber, address1, address2, city, state, zipcode) VALUES (@firstName, @lastName, @gender, @dob, @registrationDate, @phoneNumber, @address1, @address2, @city, @state, @zipcode)";

                var command = this.GetCustomerCommand(customer, query, connection);
                
                _ = command.ExecuteNonQuery();
            }
        }


        /// <summary>Gets the customers by id</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A list of the companies that could have that id (should only be one item)</returns>
        public IEnumerable<Customer> GetCustomersById(int id)
        {
            List<Customer> customerList = new List<Customer>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString)) 
            {
                connection.Open();
                string query = "select * from customer where cID = @id";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                customerList = this.GetCustomersByCommand(command, customerList);
            }
            return customerList;
        }


        /// <summary>Gets the customers by fullname.</summary>
        /// <param name="fullName">The full name.
        /// (Must be delimited by a space)</param>
        /// <returns>A list of customers with the last name and first name</returns>
        public IEnumerable<Customer> GetCustomerByFullname(string fullName) 
        {
            List<Customer> customerList = new List<Customer>();
            string[] fullNameArray = fullName.Split(' ');
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from customer where firstName = @firstName AND lastName = @lastName";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@firstName", MySqlDbType.VarChar).Value = fullNameArray[0];
                command.Parameters.Add("@lastName", MySqlDbType.VarChar).Value = fullNameArray[1];
                customerList = this.GetCustomersByCommand(command, customerList);
            }
            return customerList;
        }


        /// <summary>Gets the customers by phone number.</summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A list of customers with the phone number</returns>
        public IEnumerable<Customer> GetCustomersByPhoneNumber(string phoneNumber)
        {
            List<Customer> customerList = new List<Customer>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from customer where phoneNumber = @phoneNumber";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@phoneNumber", MySqlDbType.VarChar).Value = phoneNumber;
                customerList = this.GetCustomersByCommand(command, customerList);
            }
            return customerList;
        }


        /// <summary>Updates the customer in the database.</summary>
        /// <param name="customer">The customer.</param>
        public void UpdateCustomer(Customer customer) 
        {
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();

                string query = "update customer set firstName = @firstName, lastName = @lastName, gender = @gender, dob = @dob, phoneNumber = @phoneNumber, address1 = @address1, address2 = @address2, city = @city, state = @state, zipcode = @zipcode where cID = @cID";
                
                var command = this.GetCustomerCommand(customer, query, connection);

                command.Parameters.Add("@cID", MySqlDbType.VarChar).Value = customer.Id;

                _ = command.ExecuteNonQuery();
            }
        }


        /// <summary>Gets the customers from a constructed command query.</summary>
        /// <param name="command">The command.</param>
        /// <param name="customerList">The customer list.</param>
        /// <returns>A list of customers that correspond with the command delivered to the sql driver</returns>
        private List<Customer> GetCustomersByCommand(MySqlCommand command, List<Customer> customerList) 
        {

            using MySqlDataReader reader = command.ExecuteReader();
            int cid = reader.GetOrdinal("cID");
            int firstName = reader.GetOrdinal("firstName");
            int lastName = reader.GetOrdinal("lastName");
            int gender = reader.GetOrdinal("gender");
            int dob = reader.GetOrdinal("dob");
            int registrationDate = reader.GetOrdinal("registrationDate");
            int phoneNumber = reader.GetOrdinal("phoneNumber");
            int address1 = reader.GetOrdinal("address1");
            int address2 = reader.GetOrdinal("address2");
            int city = reader.GetOrdinal("city");
            int state = reader.GetOrdinal("state");
            int zipCode = reader.GetOrdinal("zipcode");
            while (reader.Read())
            {
                customerList.Add(new Customer
                {
                    Id = reader.GetFieldValueCheckNull<int>(cid),
                    FirstName = reader.GetFieldValueCheckNull<string>(firstName),
                    LastName = reader.GetFieldValueCheckNull<string>(lastName),
                    Gender = reader.GetFieldValueCheckNull<string>(gender),
                    BirthDay = reader.GetFieldValueCheckNull<DateTime>(dob),
                    RegistrationDate = reader.GetFieldValueCheckNull<DateTime>(registrationDate),
                    PhoneNumber = reader.GetFieldValueCheckNull<string>(phoneNumber),
                    Address1 = reader.GetFieldValueCheckNull<string>(address1),
                    Address2 = reader.GetFieldValueCheckNull<string>(address2),
                    City = reader.GetFieldValueCheckNull<string>(city),
                    State = reader.GetFieldValueCheckNull<string>(state),
                    ZipCode = reader.GetFieldValueCheckNull<string>(zipCode)
                });
            }
            return customerList;
        }


        /// <summary>Constructs a customer command from a query that uses all customer attributes except for id.</summary>
        /// <param name="customer">The customer - the customer that populates the attributes value.</param>
        /// <param name="query">The query - the query that contains the attributes.</param>
        /// <param name="connection">The connection - The sql connection to the database.</param>
        /// <returns>the command constructed from the query</returns>
        private MySqlCommand GetCustomerCommand(Customer customer, string query, MySqlConnection connection) 
        {
            using MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.Add("@firstName", MySqlDbType.VarChar).Value = customer.FirstName;
            command.Parameters.Add("@lastName", MySqlDbType.VarChar).Value = customer.LastName;
            command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = customer.Gender;
            command.Parameters.Add("@dob", MySqlDbType.Date).Value = customer.BirthDay.ToString("yyyy-MM-dd H:mm:ss");
            command.Parameters.Add("@registrationDate", MySqlDbType.Date).Value = customer.RegistrationDate.ToString("yyyy-MM-dd H:mm:ss");
            command.Parameters.Add("@phoneNumber", MySqlDbType.VarChar).Value = customer.PhoneNumber;
            command.Parameters.Add("@address1", MySqlDbType.VarChar).Value = customer.Address1;
            command.Parameters.Add("@address2", MySqlDbType.VarChar).Value = customer.Address2;
            command.Parameters.Add("@city", MySqlDbType.VarChar).Value = customer.City;
            command.Parameters.Add("@state", MySqlDbType.VarChar).Value = customer.State;
            command.Parameters.Add("@zipcode", MySqlDbType.VarChar).Value = customer.ZipCode;
            return command;
        }
    }
}
