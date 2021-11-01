using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.Extensions
{
    public static class DataReaderExtensions
    {
        public static T GetFieldValueCheckNull<T>(this MySqlDataReader reader, int colOrdinal) 
        {
            T returnValue = default;

            if (!reader[colOrdinal].Equals(DBNull.Value)) {
                returnValue = (T)reader[colOrdinal];
            }
            return returnValue;
        }
    }
}
