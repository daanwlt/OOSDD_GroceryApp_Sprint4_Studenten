namespace Grocery.Domain.ValueObjects
{
    /// <summary>
    /// Represents the role of a client in the grocery application system.
    /// Defines the access levels and permissions available to different types of users.
    /// Follows HBO-ICT coding guidelines for value objects and enums.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Default role with basic user permissions.
        /// Users with this role can only access their own grocery lists and products.
        /// </summary>
        None = 0,

        /// <summary>
        /// Administrative role with elevated permissions.
        /// Users with this role can access all features including bought products analysis.
        /// </summary>
        Admin = 1
    }
}
