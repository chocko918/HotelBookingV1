using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking2.Helpers
{
    public static class PasswordHasherHelper
    {
        public static async Task<string> HashPasswordAsync(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = await Task.Run(() => sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static async Task <bool> VerifyPassword(string hashedPassword, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hashedPasswordBytes[i])
                        return false;
                }
                return true;
            }
        }
    }
}
