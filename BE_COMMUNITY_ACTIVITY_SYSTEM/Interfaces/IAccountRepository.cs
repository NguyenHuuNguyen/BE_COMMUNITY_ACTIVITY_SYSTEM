namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> AsignRoleAsync(string userId, string roleName);
        Task<bool> RevokeRoleAsync(string userId, string roleName);
        Task<bool> ChangePasswordAsync(string userId, string newPassword);
        bool CheckLogin(string account, string password);
        bool CheckUserHasRole(string userId, string roleName);
    }
}
