using FurnitureStoreManagmentSystem.Models;

namespace FurnitureStoreManagmentSystem
{
    public static class Singletons
    {
        #region Properties

        public static Employee CurrentEmployee { get; set; }

        public static int CurrentTransaction { get; set; } = 0;

        #endregion
    }
}