namespace FurnitureStoreManagmentSystem.Models
{
    /// <summary>Represents an furniture object in the database</summary>
    /// <author>Daniel Crumpler</author>
    /// <version>Fall 2021</version>
    public class Furniture
    {
        #region Properties

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the item name.</summary>
        /// <value>The item name.</value>
        public string ItemName { get; set; }

        /// <summary>Gets or sets the item description.</summary>
        /// <value>The item description.</value>
        public string ItemDescription { get; set; }

        /// <summary>Gets or sets the style name.</summary>
        /// <value>The style name.</value>
        public string StyleName { get; set; }

        /// <summary>Gets or sets the category name.</summary>
        /// <value>The category name.</value>
        public string CategoryName { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public double Price { get; set; }

        #endregion
    }
}