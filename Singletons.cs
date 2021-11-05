using System.Collections.Generic;
using FurnitureStoreManagmentSystem.Models;

namespace FurnitureStoreManagmentSystem
{
    public static class Singletons
    {
        #region Properties

        public static Employee CurrentEmployee { get; set; }

        public static int CurrentTransaction { get; set; } = 0;

        public static List<Furniture> FurnitureCart { get; set; } = new List<Furniture>();

        public static Customer CurrentCustomer { get; set; }

        public static int Quantity { get; set; } = 0;

        public static double TotalCost { get; set; } = 0;

        #endregion
    }
}