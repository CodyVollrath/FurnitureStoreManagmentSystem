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
    }
}
