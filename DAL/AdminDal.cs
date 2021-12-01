using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FurnitureStoreManagmentSystem.DAL
{
    public static class AdminDal
    {
        public static string AdminCommand(string query) 
        {
            var output = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
                {
                    connection.Open();
                    using MySqlCommand command = new MySqlCommand(query, connection);
                    using MySqlDataReader reader = command.ExecuteReader();
                    output = getColumnNames(reader);
                    while (reader.Read())
                    {
                        var delimiter = ",";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (i == reader.FieldCount - 1) {
                                delimiter = "\n";
                            }
                            output += $"{reader[i]}{delimiter}";
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                output = exception.Message;
            }

            return output;
        }

        public static string ReportCommand(DateTime startDate, DateTime endDate)
        {
            var output = "Report of people that have rented furniture and the furniture that they have rented:\n\n";
            var query = 
                "select distinct C.cID, CONCAT(C.firstName, ' ' ,C.lastName) as fullName, F.itemName, F.itemDescription, IO.fQuantity" +
                " " +
                "from customer C, item_check_out IO, transaction T, furniture F, rental R " +
                "where IO.tID = T.tID AND T.cID = C.cID AND F.fID = IO.fID AND T.tID = R.tID AND R.rentalDate BETWEEN @startDate AND @endDate";
            List<string> vistedCustomerNames = new List<string>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
                {
                    connection.Open();
                    using MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@startDate", MySqlDbType.Date).Value = startDate;
                    command.Parameters.Add("@endDate", MySqlDbType.Date).Value = endDate;
                    using MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var customerName = (string)reader[1];
                        if (!vistedCustomerNames.Contains(customerName))
                        {
                            vistedCustomerNames.Add(customerName);
                            output += $"Id: {reader[0]} | Name: {customerName}\nFurniture Rented\n";
                        }
                        output += $"\tName: {reader[2]}\n\tDescription: {reader[3]}\n\tQty: {reader[4]}\n";
                        
                    }
                }
            }
            catch (Exception exception)
            {
                output = exception.Message;
            }
            return output;
        }

        public static void AddEmployee(Employee employee) 
        {
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                using MySqlCommand command = new MySqlCommand("CreateEmployee", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@eID", MySqlDbType.Int32);
                command.Parameters["@eID"].Value = employee.Id;
                command.Parameters["@eID"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@username", MySqlDbType.VarChar);
                command.Parameters["@username"].Value = employee.Username.ToLower();
                command.Parameters["@username"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@password", MySqlDbType.VarChar);
                command.Parameters["@password"].Value = Hasher.Encrypt(employee.Password);
                command.Parameters["@password"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@firstName", MySqlDbType.VarChar);
                command.Parameters["@firstName"].Value = employee.FirstName;
                command.Parameters["@firstName"].Direction = System.Data.ParameterDirection.Input;


                command.Parameters.Add("@lastName", MySqlDbType.VarChar);
                command.Parameters["@lastName"].Value = employee.Lastname;
                command.Parameters["@lastName"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@admin", MySqlDbType.Int32);
                command.Parameters["@admin"].Value = employee.IsAdmin ? 1 : 0;
                command.Parameters["@admin"].Direction = System.Data.ParameterDirection.Input;
                _ = command.ExecuteNonQuery();
            }
        }

        private static string getColumnNames(MySqlDataReader reader) 
        {
            var output = "";
            var delimiter = ",";
            for (int i = 0; i < reader.FieldCount; i++) {
                if (i == reader.FieldCount - 1) {
                    delimiter = "\n";
                }
                output += $"{reader.GetName(i)}{delimiter}";
            }
            return output;
        }
    }
}
