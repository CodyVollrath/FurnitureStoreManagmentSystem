
namespace FurnitureStoreManagmentSystem.Models
{

    /// <summary>Represents an employee object in the database</summary>
    public class Employee
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the lastname.</summary>
        /// <value>The lastname.</value>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the IsAdmin.
        /// </summary>
        /// <value>The true or false indicator for admin status</value>
        public bool IsAdmin { get; set; }
    }
}
