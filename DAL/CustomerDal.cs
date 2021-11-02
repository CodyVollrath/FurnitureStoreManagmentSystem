using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.DAL
{
    public class CustomerDal
    {
        public void CreateCustomer(Customer customer)
        {
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "insert into customer (firstName, lastName, gender, dob, registrationDate, phoneNumber, address1, address2, city, state, zipcode) VALUES (@firstName, @lastName, @gender, @dob, @registrationDate, @phoneNumber, @address1, @address2, @city, @state, @zipcode)";

                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@firstName", MySqlDbType.VarChar).Value = customer.FirstName;
                command.Parameters.Add("@lastName", MySqlDbType.VarChar).Value = customer.LastName;
                command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = customer.Gender?.ToCharArray()[0];
                command.Parameters.Add("@dob", MySqlDbType.Date).Value = customer.BirthDay.ToString("yyyy-MM-dd H:mm:ss");
                command.Parameters.Add("@registrationDate", MySqlDbType.Date).Value = customer.RegistrationDate.ToString("yyyy-MM-dd H:mm:ss");
                command.Parameters.Add("@phoneNumber", MySqlDbType.VarChar).Value = customer.PhoneNumber;
                command.Parameters.Add("@address1", MySqlDbType.VarChar).Value = customer.Address1;
                command.Parameters.Add("@address2", MySqlDbType.VarChar).Value = customer.Address2;
                command.Parameters.Add("@city", MySqlDbType.VarChar).Value = customer.City;
                command.Parameters.Add("@state", MySqlDbType.VarChar).Value = customer.State;
                command.Parameters.Add("@zipcode", MySqlDbType.VarChar).Value = customer.ZipCode;

                _ = command.ExecuteNonQuery();
            }
        }
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
    }
}
