using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using System.Security.Cryptography;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
