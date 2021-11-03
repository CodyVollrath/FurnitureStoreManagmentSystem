using MySql.Data.MySqlClient;
using System;

namespace FurnitureStoreManagmentSystem.Extensions
{

    /// <summary>Extensions for common data reader operations</summary>
    public static class DataReaderExtensions
    {

        /// <summary>Gets the field value and checks if null.</summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="reader">The reade - the sql reader to determine the field value.</param>
        /// <param name="colOrdinal">The col ordinal - the order value of the column.</param>
        /// <returns>the value of the field</returns>
        public static T GetFieldValueCheckNull<T>(this MySqlDataReader reader, int colOrdinal) 
        {
            T fieldValue = default;

            if (!reader[colOrdinal].Equals(DBNull.Value)) {
                fieldValue = (T)reader[colOrdinal];
            }
            return fieldValue;
        }
    }
}
