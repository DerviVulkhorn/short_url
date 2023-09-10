using System.Security.Cryptography;
using System.Text;

namespace ShortLink.Script
{
    public class HashS
    {
        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
        private bool ValidatePassword(string password, string passwordHash)
        {
            var sha256 = SHA256.Create();
            var hashedInputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedInput = BitConverter.ToString(hashedInputBytes).Replace("-", "").ToLower();

            return passwordHash == hashedInput;
        }
    }
}
