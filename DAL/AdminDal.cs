﻿using FurnitureStoreManagmentSystem.Models;
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
    }
}
