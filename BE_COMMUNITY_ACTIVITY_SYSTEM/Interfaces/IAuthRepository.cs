using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> CreateToken(User user);
        Task<bool> AsignRoleAsync(string userId, string roleName);
        Task<bool> RevokeRoleAsync(string userId, string roleName);
        Task<bool> ChangePasswordAsync(string userId, string newPassword);
        Task<ICollection<string>> GetRolesOfUser(string userId);
        Task<bool> CheckLogin(string account, string password);
        bool CheckUserHasRole(string userId, string roleName);
        bool CheckRoleExists(string roleName);
    }
}
