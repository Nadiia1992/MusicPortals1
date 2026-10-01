using MusicPortal.Models;
using System.Security.Cryptography;
using System.Text;


namespace MusicPortal.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(salt + password);
            return Convert.ToHexString(SHA256.HashData(passwordBytes));
        }

        public bool VerifyPassword(string password, string salt, string hash)
        {
            var passwordHash = HashPassword(password, salt);
            return passwordHash == hash;
        }
    }
}

