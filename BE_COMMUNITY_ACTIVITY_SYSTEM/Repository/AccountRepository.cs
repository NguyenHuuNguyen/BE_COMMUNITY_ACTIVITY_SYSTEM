using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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
        public async Task<bool> AsignRoleAsync(string userId, string roleName)
        {
            var roleUser = await _context.RoleUsers.Include(x => x.Role).FirstOrDefaultAsync(x => userId.Equals(x.UserId)
                                                                                            && roleName.Equals(x.Role!.RoleName)
                                                                                            && x.IsDeleted == false);

            if (roleUser == null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => userId.Equals(x.Id) && x.IsDeleted == false);
                var role = await _context.Roles.FirstOrDefaultAsync(x => roleName.Equals(x.RoleName) && x.IsDeleted == false);

                if (user == null || role == null)
                {
                    return false;
                }

                var newRoleUser = new RoleUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    RoleId = role.Id
                };
                await _context.RoleUsers.AddAsync(newRoleUser);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                if (roleUser.IsDeleted)
                {
                    roleUser.IsDeleted = false;
                    return await _context.SaveChangesAsync() > 0;
                }
                return true;
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => userId.Equals(x.Id) && x.IsDeleted == false);

            if (user == null)
            {
                return false;
            }

            _commonRepository.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return await _context.SaveChangesAsync() > 0;
        }

        public bool CheckLogin(string account, string password)
        {
            var user = _context.Users.FirstOrDefault(x => account.Equals(x.StudentId) || account.Equals(x.TeacherId));

            if (user == null || user.PasswordHash == null || user.PasswordSalt == null)
            {
                return false;
            }

            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        public bool CheckRoleExists(string roleName)
        {
            return _context.Roles.Any(x => roleName.Equals(x.RoleName) && x.IsDeleted == false);
        }

        public bool CheckUserHasRole(string userId, string roleName)
        {
            return _context.RoleUsers.Any(ru => userId.Equals(ru.UserId) && ru.IsDeleted == false && roleName.Equals(ru.Role!.RoleName));
        }

        public async Task<bool> RevokeRoleAsync(string userId, string roleName)
        {
            var roleUser = await _context.RoleUsers.Include(x => x.Role).FirstOrDefaultAsync(x => userId.Equals(x.UserId)
                                                                                            && roleName.Equals(x.Role!.RoleName)
                                                                                            && x.IsDeleted == false);

            if (roleUser == null)
            {
                return false;
            }

            roleUser.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<string>> GetRolesOfUser(string userId)
        {
            var roles = await _context.RoleUsers
                                        .Include(ru => ru.Role)
                                        .Where(x => userId.Equals(x.UserId) && !x.IsDeleted)
                                        .Select(x => x.Role!.RoleName!)
                                        .ToListAsync();

            return roles ?? new List<string>();
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
