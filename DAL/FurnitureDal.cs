using System.Collections.Generic;
using FurnitureStoreManagmentSystem.Extensions;
using FurnitureStoreManagmentSystem.Models;
using FurnitureStoreManagmentSystem.Resources;
using MySql.Data.MySqlClient;

namespace FurnitureStoreManagmentSystem.DAL
{
    /// <summary>Performs SQL operations on the Furniture table</summary>
    /// <author>Daniel Crumpler & Cody Vollrath</author>
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
                using MySqlCommand command = new MySqlCommand("create_item_checkout", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@fIDp", MySqlDbType.Int32);
                command.Parameters["@fIDp"].Value = fID;
                command.Parameters["@fIDp"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@tIDp", MySqlDbType.Int32);
                command.Parameters["@tIDp"].Value = tID;
                command.Parameters["@tIDp"].Direction = System.Data.ParameterDirection.Input;

                command.Parameters.Add("@fQuantityp", MySqlDbType.Int32);
                command.Parameters["@fQuantityp"].Value = quantity;
                command.Parameters["@fQuantityp"].Direction = System.Data.ParameterDirection.Input;
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

        /// <summary>Creates a rental based upon a transaction</summary>
        public void CreateRental(int tID, double cost)
        {
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "insert into rental (tID, estimatedCost, estimatedFees, rentalDate, dueDate) VALUES (@tID, @cost, 1.00, '2021-11-05', '2021-11-15')";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@tID", MySqlDbType.VarChar).Value = tID;
                command.Parameters.Add("@cost", MySqlDbType.VarChar).Value = cost;
                _ = command.ExecuteNonQuery();
            }
        }

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
                Singletons.CurrentTransaction = (int)command.LastInsertedId;
            }
        }

        public IEnumerable<string> GetCategories() 
        {
            List<string> categories = new List<string>();
            using (var connection = new MySqlConnection(Constants.ConnectionString)) 
            {
                connection.Open();
                var query = "select categoryName from category";
                using var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var categoryName = reader.GetOrdinal("categoryName");
                while (reader.Read()) 
                {
                    categories.Add(reader.GetFieldValueCheckNull<string>(categoryName));
                }
            }

            return categories;
        }

        internal List<Furniture> GetFurnitureInRentals(int id)
        {
            var furnitureList = new List<Furniture>();
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select * from furniture, item_check_out where item_check_out.fID=furniture.fID and item_check_out.tID=@id";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                furnitureList = this.GetFurnitureByCommandWithIDs(command, furnitureList);
            }

            return furnitureList;
        }

        public List<int> GetRentals(int id)
        {
            List<int> rentals = new List<int>();
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select item_check_out.tID from transaction, item_check_out where transaction.cID=@id and transaction.tID=item_check_out.tID GROUP BY item_check_out.tID";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                var reader = command.ExecuteReader();
                var tID = reader.GetOrdinal("tID");
                while (reader.Read())
                {
                    rentals.Add(reader.GetFieldValueCheckNull<int>(tID));
                }
            }

            return rentals;
        }

        /// <summary>
        /// Gets the style names from the styles table
        /// </summary>
        /// <returns>An enumerated list of style names</returns>
        public IEnumerable<string> GetStyles()
        {
            List<string> styles = new List<string>();
            using (var connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var query = "select styleName from style";
                using var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                var styleName = reader.GetOrdinal("styleName");
                while (reader.Read())
                {
                    styles.Add(reader.GetFieldValueCheckNull<string>(styleName));
                }
            }
            return styles;
        }

        #endregion

        #region Searchs
        /// <summary>
        /// Gets a list of furnitures by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An enumerated list of furniture</returns>
        public IEnumerable<Furniture> GetFurnituresById(int id)
        {
            List<Furniture> furnitureList = new List<Furniture>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from furniture where fID = @id";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                furnitureList = this.GetFurnitureByCommand(command, furnitureList);
            }
            return furnitureList;
        }
        /// <summary>
        /// Gets a list of furnitures by the itme name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An enumerated list of furniture</returns>
        public IEnumerable<Furniture> GetFurnituresByName(string name)
        {
            List<Furniture> furnitureList = new List<Furniture>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from furniture where itemName = @name";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                furnitureList = this.GetFurnitureByCommand(command, furnitureList);
            }
            return furnitureList;
        }

        /// <summary>
        /// Gets a list of furnitures by the style name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An enumerated list of furniture</returns>
        public IEnumerable<Furniture> GetFurnituresByStyleName(string styleName)
        {
            List<Furniture> furnitureList = new List<Furniture>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from furniture where styleName = @styleName";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@styleName", MySqlDbType.VarChar).Value = styleName;
                furnitureList = this.GetFurnitureByCommand(command, furnitureList);
            }
            return furnitureList;
        }

        /// <summary>
        /// Gets a list of furnitures by the category name
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An enumerated list of furniture</returns>
        public IEnumerable<Furniture> GetFurnituresByCategoryName(string categoryName)
        {
            List<Furniture> furnitureList = new List<Furniture>();
            using (MySqlConnection connection = new MySqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                string query = "select * from furniture where categoryName = @categoryName";
                using MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@categoryName", MySqlDbType.VarChar).Value = categoryName;
                furnitureList = this.GetFurnitureByCommand(command, furnitureList);
            }
            return furnitureList;
        }
        #endregion

        #region Private helpers
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
            var price = reader.GetOrdinal("daily_rental_rate");
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
                    Price = reader.GetFieldValueCheckNull<double>(price)
                });
            }

            return furnitureList;
        }

        /// <summary>Gets the furniture from a constructed command query.</summary>
        /// <param name="command">The command.</param>
        /// <param name="furnitureList">The customer list.</param>
        /// <returns>A list of furniture that correspond with the command delivered to the sql driver</returns>
        private List<Furniture> GetFurnitureByCommandWithIDs(MySqlCommand command, List<Furniture> furnitureList)
        {
            using var reader = command.ExecuteReader();
            var fId = reader.GetOrdinal("fID");
            var itemName = reader.GetOrdinal("itemName");
            var itemDescription = reader.GetOrdinal("itemDescription");
            var styleName = reader.GetOrdinal("styleName");
            var categoryName = reader.GetOrdinal("categoryName");
            var quantity = reader.GetOrdinal("fQuantity");
            var price = reader.GetOrdinal("daily_rental_rate");
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
                    Price = reader.GetFieldValueCheckNull<double>(price)
                });
            }

            return furnitureList;
        }
        #endregion

    }
}