using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FurnitureStoreManagmentSystem.DAL
{

    /// <summary>Performs SQL operations on the Furniture table</summary>
    /// <author>Daniel Crumpler</author>
    /// <version>Fall 2021</version>
    public class FurnitureDal
    {

        public IEnumerable<Furniture> GetFurniture()
        {
            List<Furniture> furnitureList = new List<Furniture>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from furniture";
                using MySqlCommand command = new MySqlCommand(query, connection);
                furnitureList = this.GetFurnitureByCommand(command, furnitureList);
            }
            return furnitureList;
        }

        /// <summary>Gets the furniture from a constructed command query.</summary>
        /// <param name="command">The command.</param>
        /// <param name="furnitureList">The customer list.</param>
        /// <returns>A list of customers that correspond with the command delivered to the sql driver</returns>
        private List<Furniture> GetFurnitureByCommand(MySqlCommand command, List<Furniture> furnitureList)
        {

            using MySqlDataReader reader = command.ExecuteReader();
            int fId = reader.GetOrdinal("fID");
            int itemName = reader.GetOrdinal("itemName");
            int itemDescription = reader.GetOrdinal("itemDescription");
            int styleName = reader.GetOrdinal("styleName");
            int categoryName = reader.GetOrdinal("categoryName");
            int quantity = reader.GetOrdinal("quantity");
            while (reader.Read())
            {
                furnitureList.Add(new Furniture
                {
                    Id = reader.GetFieldValueCheckNull<int>(fId),
                    ItemName = reader.GetFieldValueCheckNull<string>(itemName),
                    ItemDescription = reader.GetFieldValueCheckNull<string>(itemDescription),
                    StyleName = reader.GetFieldValueCheckNull<string>(styleName),
                    CategoryName = reader.GetFieldValueCheckNull<string>(categoryName),
                    Quantity = reader.GetFieldValueCheckNull<int>(quantity),
                });
            }
            return furnitureList;
        }
    }
}
