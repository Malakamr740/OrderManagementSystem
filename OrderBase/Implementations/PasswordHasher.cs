using OrderBase.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace OrderBase.Implementations
{
    public class PasswordHasher : IHasher
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static PasswordHasher()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public (string,string) GenerateHash(string plainText)
        {
            var salt = GenerateSalt();
            var passwordHash = HashPassword(plainText,salt);
            return (salt, passwordHash);
            //using (var sha256 = new SHA256Managed())
            //{
            //    var salt = GenerateSalt();   
            //    byte[] passwordBytes = Encoding.UTF8.GetBytes(plainText);
            //    byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            //    Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            //    Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            //    // Hash the concatenated password and salt
            //    byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            //    // Concatenate the salt and hashed password for storage
            //    byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            //    Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            //    Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            //    return (Convert.ToBase64String(hashedPasswordWithSalt) , salt.ToString());
            //}
        }

        // Verify the password by comparing hashes
        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var enteredPasswordHash = HashPassword(enteredPassword, storedSalt);
            return storedHash == enteredPasswordHash;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32]; // 256 bits
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

       
        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Combine password and salt
                var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(combinedBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
