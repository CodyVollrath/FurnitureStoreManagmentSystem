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

        /// <summary>Creates a new item_check_out based upon a piece of furniture, quantity, and transaction</summary>
        public void CreateItemCheckOut(int fID, int tID, int quantity)
        {
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "insert into item_check_out (fID, tID, fQuantity) VALUES (@fID, @tID, @fQuantity)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@fID", MySqlDbType.VarChar).Value = fID;
                command.Parameters.Add("@tID", MySqlDbType.VarChar).Value = tID;
                command.Parameters.Add("@fQuantity", MySqlDbType.VarChar).Value = quantity;
                _ = command.ExecuteNonQuery();
            }
        }

        /// <summary>Modifies the current furniture quantity</summary>
        public void ModifyFurnitureQuantity(int fID, int quantity)
        {
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query =
                    "UPDATE furniture SET quantity = (SELECT furniture.quantity WHERE fID = @fID) - @fQuantity WHERE fID = @fID";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@fID", MySqlDbType.VarChar).Value = fID;
                command.Parameters.Add("@fQuantity", MySqlDbType.VarChar).Value = quantity;
                _ = command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Methods

        /// <summary>Gets all of the furniture from a constructed command query.</summary>
        /// <returns>A list of furniture that correspond with the command delivered to the sql driver</returns>
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
        /// <returns>A list of furniture that correspond with the command delivered to the sql driver</returns>
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

        /// <summary>Creates a new transaction based upon a selected customer</summary>
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