using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly ICommonRepository _commonRepository;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, ICommonRepository commonRepository, IConfiguration configuration)
        {
            _context = context;
            _commonRepository = commonRepository;
            _configuration = configuration;
        }

        public async Task<string> CreateToken(User user)
        {
            bool isStudent = false;
            if (user.StudentId != null && user.TeacherId == null)
            {
                isStudent = true;
            }

            ICollection<string> roles = await GetRolesOfUser(user.Id!);

            List<Claim> claims = new()
            {
                new Claim("UserId", user.Id!),
                new Claim("FirstName", user.FirstName!),
                new Claim("IsStudent", isStudent.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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

        public async Task<bool> CheckLogin(string account, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => account.Equals(x.StudentId) || account.Equals(x.TeacherId));

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
