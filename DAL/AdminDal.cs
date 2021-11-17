using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System;

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

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            output += $"{reader[i]} ";
                        }
                        output += "\n";
                    }
                }
            }
            catch(Exception exception)
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
                string query = "call CreateEmployee(@eID, @username, @password, @firstName, @lastName, @admin)";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@eID", MySqlDbType.Int32).Value = employee.Id;
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = employee.Username.ToLower();
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Hasher.Encrypt(employee.Password);
                command.Parameters.Add("@firstName", MySqlDbType.VarChar).Value = employee.FirstName;
                command.Parameters.Add("@lastName", MySqlDbType.VarChar).Value = employee.Lastname;
                command.Parameters.Add("@admin", MySqlDbType.Int32).Value = employee.IsAdmin ? 1 : 0;
                _ = command.ExecuteNonQuery();
            }
        }
    }
}
