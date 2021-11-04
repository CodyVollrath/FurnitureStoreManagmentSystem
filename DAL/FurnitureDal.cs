using System.Collections.Generic;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;

namespace FurnitureStoreManagmentSystem.DAL
{
    /// <summary>Performs SQL operations on the Furniture table</summary>
    /// <author>Daniel Crumpler</author>
    /// <version>Fall 2021</version>
    public class FurnitureDal
    {
        #region Methods

        public IEnumerable<Furniture> GetFurniture()
        {
            var furnitureList = new List<Furniture>();
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select * from furniture";
                using var command = new MySqlCommand(query, connection);
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
            using var reader = command.ExecuteReader();
            var fId = reader.GetOrdinal("fID");
            var itemName = reader.GetOrdinal("itemName");
            var itemDescription = reader.GetOrdinal("itemDescription");
            var styleName = reader.GetOrdinal("styleName");
            var categoryName = reader.GetOrdinal("categoryName");
            var quantity = reader.GetOrdinal("quantity");
            while (reader.Read())
            {
                furnitureList.Add(new Furniture
                {
                    Id = reader.GetFieldValueCheckNull<int>(fId),
                    ItemName = reader.GetFieldValueCheckNull<string>(itemName),
                    ItemDescription = reader.GetFieldValueCheckNull<string>(itemDescription),
                    StyleName = reader.GetFieldValueCheckNull<string>(styleName),
                    CategoryName = reader.GetFieldValueCheckNull<string>(categoryName),
                    Quantity = reader.GetFieldValueCheckNull<int>(quantity)
                });
            }

            return furnitureList;
        }

        public void CreateTransaction(int eID, int cID)
        {
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "insert into transaction (eID, cID) VALUES (@eID, @cID)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@eID", MySqlDbType.VarChar).Value = eID;
                command.Parameters.Add("@cID", MySqlDbType.VarChar).Value = cID;
                _ = command.ExecuteNonQuery();
                Singletons.CurrentTransaction = (int) command.LastInsertedId;
            }

            #endregion
        }
    }
}