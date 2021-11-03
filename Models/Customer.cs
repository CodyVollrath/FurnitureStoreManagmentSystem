using System;

namespace FurnitureStoreManagmentSystem.Models
{

    /// <summary>The customer model that represents a customer in the database</summary>
    /// <author>Cody Vollrath</author>
    public class Customer
    {

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; } = "";

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        public string LastName { get; set; } = "";

        /// <summary>Gets or sets the gender.</summary>
        /// <value>The gender.</value>
        public string Gender { get; set; } = "";

        /// <summary>Gets or sets the birth day.</summary>
        /// <value>The birth day.</value>
        public DateTime BirthDay { get; set; } = DateTime.Now;

        /// <summary>Gets or sets the registration date.</summary>
        /// <value>The registration date.</value>
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; } = "";

        /// <summary>Gets or sets the address1.</summary>
        /// <value>The address1.</value>
        public string Address1 { get; set; } = "";

        /// <summary>Gets or sets the address2.</summary>
        /// <value>The address2.</value>
        public string Address2 { get; set; } = "";

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        public string City { get; set; } = "";

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public string State { get; set; } = "";

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        public string ZipCode { get; set; } = "";
    }
}
