using System.Security.Cryptography;
using System.Text;

namespace Grocery.Domain.Helpers
{
    /// <summary>
    /// Static helper class for password hashing and verification operations.
    /// Implements secure password hashing using PBKDF2 with salt.
    /// Follows HBO-ICT coding guidelines for security and utility classes.
    /// </summary>
    public static class PasswordHelper
    {
        #region Constants

        /// <summary>
        /// The size of the salt in bytes for password hashing.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// The size of the hash in bytes for password hashing.
        /// </summary>
        private const int HashSize = 32;

        /// <summary>
        /// The number of iterations for PBKDF2 password hashing.
        /// Higher values provide better security but require more computation time.
        /// </summary>
        private const int Iterations = 10000;

        #endregion

        #region Public Methods

        /// <summary>
        /// Hashes a password using PBKDF2 with a random salt.
        /// This method provides secure password storage by combining a random salt with the password.
        /// </summary>
        /// <param name="password">The plain text password to hash</param>
        /// <returns>A base64-encoded string containing the salt and hash</returns>
        /// <exception cref="ArgumentException">Thrown when password is null or empty</exception>
        public static string HashPassword(string password)
        {
            // Validate input according to HBO-ICT guidelines
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            // Generate a random salt for this password
            byte[] salt = GenerateRandomSalt();

            // Hash the password with the salt using PBKDF2
            byte[] hash = HashPasswordWithSalt(password, salt);

            // Combine salt and hash into a single byte array
            byte[] hashBytes = CombineSaltAndHash(salt, hash);

            // Return the combined salt and hash as a base64 string
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifies a password against a stored hash.
        /// This method securely compares the provided password with the stored hash.
        /// </summary>
        /// <param name="password">The plain text password to verify</param>
        /// <param name="hashedPassword">The stored hash to compare against</param>
        /// <returns>True if the password matches the hash; otherwise, false</returns>
        /// <exception cref="ArgumentException">Thrown when parameters are null or empty</exception>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Validate input according to HBO-ICT guidelines
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hashed password cannot be null or empty", nameof(hashedPassword));

            try
            {
                // Decode the stored hash
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);

                // Extract the salt from the stored hash
                byte[] salt = ExtractSaltFromHash(hashBytes);

                // Hash the provided password with the extracted salt
                byte[] hash = HashPasswordWithSalt(password, salt);

                // Compare the computed hash with the stored hash
                return CompareHashes(hashBytes, hash);
            }
            catch (FormatException)
            {
                // Invalid base64 format
                return false;
            }
            catch (ArgumentException)
            {
                // Invalid hash format
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        /// <returns>A byte array containing the random salt</returns>
        private static byte[] GenerateRandomSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Hashes a password with the provided salt using PBKDF2.
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <param name="salt">The salt to use for hashing</param>
        /// <returns>A byte array containing the hash</returns>
        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Combines salt and hash into a single byte array.
        /// </summary>
        /// <param name="salt">The salt bytes</param>
        /// <param name="hash">The hash bytes</param>
        /// <returns>A combined byte array with salt followed by hash</returns>
        private static byte[] CombineSaltAndHash(byte[] salt, byte[] hash)
        {
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }

        /// <summary>
        /// Extracts the salt from a combined salt and hash byte array.
        /// </summary>
        /// <param name="hashBytes">The combined salt and hash bytes</param>
        /// <returns>The extracted salt bytes</returns>
        /// <exception cref="ArgumentException">Thrown when hashBytes is too short</exception>
        private static byte[] ExtractSaltFromHash(byte[] hashBytes)
        {
            if (hashBytes.Length < SaltSize + HashSize)
                throw new ArgumentException("Invalid hash format", nameof(hashBytes));

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            return salt;
        }

        /// <summary>
        /// Compares two hash values securely to prevent timing attacks.
        /// </summary>
        /// <param name="storedHash">The stored hash bytes</param>
        /// <param name="computedHash">The computed hash bytes</param>
        /// <returns>True if the hashes match; otherwise, false</returns>
        private static bool CompareHashes(byte[] storedHash, byte[] computedHash)
        {
            if (storedHash.Length < SaltSize + HashSize)
                return false;

            // Compare only the hash portion (skip the salt)
            for (int i = 0; i < HashSize; i++)
            {
                if (storedHash[i + SaltSize] != computedHash[i])
                    return false;
            }
            return true;
        }

        #endregion
    }
}
