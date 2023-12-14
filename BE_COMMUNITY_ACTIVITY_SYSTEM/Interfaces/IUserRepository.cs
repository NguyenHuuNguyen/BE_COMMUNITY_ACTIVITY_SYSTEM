using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<ICollection<User>> GetUsersAsync();
        Task<ICollection<User>> GetUsersByClassIdAsync(string classId);
        Task<User> CreateUserAsync(UserCreateDto user);
        Task<User> UpdateUserAsync(UserUpdateDto user);
        Task<User> UpdateUserStatusAsync(string userId, int status);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> CheckUserExistsAsync(string userId);
        bool CheckUserExists(string userId);
        bool CheckUserIsStudent(string userId);
    }
}
