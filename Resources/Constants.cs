using System.Collections.Generic;

namespace FurnitureStoreManagmentSystem.Resources
{

    /// <summary>Keeps track of commonly used constants in the function</summary>
    public static class Constants
    {
        public static readonly string ConnectionString = "server=160.10.217.6; port=3306; uid=cvollra1; pwd=1iN9k0zA!j; database=cs3230f21h;";
        public static readonly List<string> States = new List<string> {

                "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware",
                "District of Columbia", "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa",
                "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota",
                "Minor Outlying Islands", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire",
                "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands",
                "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota",
                "Tennessee", "Texas", "U.S. Virgin Islands", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
            };
        public static readonly List<string> Genders = new List<string> { "M", "F" };
    }
}
