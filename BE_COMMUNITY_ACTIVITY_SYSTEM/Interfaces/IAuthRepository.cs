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
        Task<bool> CheckUserHasRoleAsync(string userId, string roleName);
        bool CheckRoleExists(string roleName);
        // Chỉ cho phép admin hoặc chính user đó cập nhật thông tin của mình.
        Task<bool> CheckUserAuthorizedForActionAsync(string tokenUserId, string targetUserId);
        Task<bool> CheckAccountLockedAsync(string account);
    }
}
