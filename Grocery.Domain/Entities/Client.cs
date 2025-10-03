
using System.ComponentModel.DataAnnotations;
using Grocery.Domain.ValueObjects;

namespace Grocery.Domain.Entities
{
    /// <summary>
    /// Represents a client (user) in the grocery application system.
    /// Contains user authentication information and role-based access control.
    /// Follows HBO-ICT coding guidelines for domain entities and data validation.
    /// </summary>
    public partial class Client : Model
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email address of the client.
        /// This property is required and must be a valid email format.
        /// </summary>
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password of the client.
        /// This property contains the encrypted password and should never be stored in plain text.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role of the client in the system.
        /// Determines what actions the client can perform.
        /// </summary>
        public Role Role { get; set; } = Role.None;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        /// <param name="id">The unique identifier for the client</param>
        /// <param name="name">The display name of the client</param>
        /// <param name="emailAddress">The email address of the client</param>
        /// <param name="password">The hashed password of the client</param>
        /// <exception cref="ArgumentException">Thrown when required parameters are null or empty</exception>
        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            // Validate input parameters according to HBO-ICT guidelines
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ArgumentException("Email address cannot be null or empty", nameof(emailAddress));
            
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            EmailAddress = emailAddress;
            Password = password;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines if the client has admin privileges.
        /// </summary>
        /// <returns>True if the client has admin role; otherwise, false</returns>
        public bool IsAdmin()
        {
            return Role == Role.Admin;
        }

        /// <summary>
        /// Determines if the client has the specified role.
        /// </summary>
        /// <param name="requiredRole">The role to check for</param>
        /// <returns>True if the client has the specified role; otherwise, false</returns>
        public bool HasRole(Role requiredRole)
        {
            return Role == requiredRole;
        }

        #endregion
    }
}
