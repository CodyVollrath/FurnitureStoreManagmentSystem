using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.DAL
{
    public static class AdminDal
    {
        public static string AdminCommand(string query) 
        {
            var output = "";
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
            return output;
        }
    }
}
