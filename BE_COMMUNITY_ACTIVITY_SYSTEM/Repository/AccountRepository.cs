using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly ICommonRepository _commonRepository;

        public AccountRepository(DataContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }
        public Task<bool> AsignRoleAsync(string userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => userId.Equals(x.Id));

            if (user == null)
            {
                return false;
            }

            byte[] passwordHash, passwordSalt;
            _commonRepository.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
            return true;

            ///code tiep thoi
        }

        public bool CheckLogin(string account, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserHasRole(string userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeRoleAsync(string userId, string roleName)
        {
            throw new NotImplementedException();
        }

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
