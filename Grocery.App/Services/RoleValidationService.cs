using Grocery.Domain.Entities;
using Grocery.Domain.ValueObjects;

namespace Grocery.App.Services
{
    /// <summary>
    /// Service responsible for validating user roles and permissions.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// role-based access control validation.
    /// </summary>
    public class RoleValidationService
    {
        /// <summary>
        /// Validates if a client has admin role.
        /// This method has a single responsibility: admin role validation.
        /// </summary>
        /// <param name="client">The client to validate</param>
        /// <returns>True if client has admin role, false otherwise</returns>
        public bool IsAdmin(Client client)
        {
            return client.Role == Role.Admin;
        }

        /// <summary>
        /// Validates if a client has the required role.
        /// This method has a single responsibility: role validation.
        /// </summary>
        /// <param name="client">The client to validate</param>
        /// <param name="requiredRole">The required role</param>
        /// <returns>True if client has the required role, false otherwise</returns>
        public bool HasRole(Client client, Role requiredRole)
        {
            return client.Role == requiredRole;
        }

        /// <summary>
        /// Validates if a client can access admin features.
        /// This method has a single responsibility: admin access validation.
        /// </summary>
        /// <param name="client">The client to validate</param>
        /// <returns>True if client can access admin features, false otherwise</returns>
        public bool CanAccessAdminFeatures(Client client)
        {
            return IsAdmin(client);
        }
    }
}
