using System.Collections.Generic;
using System.Collections.ObjectModel;
using FurnitureStoreManagmentSystem.Models;

namespace FurnitureStoreManagmentSystem.Extensions
{
    /// <summary>Handles Extensions for Customers</summary>
    /// <author>Daniel Crumpler</author>
    public static class FurnitureExtensions
    {
        #region Methods
        /// <summary>Converts an Enumerable collection of furniture to an observable collection.</summary>
        /// <param name="furnitures">The customers - the collection to be converted to an observable list.</param>
        /// <returns>The observable collection of furniture</returns>
        public static ObservableCollection<Furniture> ConvertToObservable(this IEnumerable<Furniture> furnitures)
        {
            var observableFurnitures = new ObservableCollection<Furniture>(furnitures);
            return observableFurnitures;
        }

        #endregion
    }
}