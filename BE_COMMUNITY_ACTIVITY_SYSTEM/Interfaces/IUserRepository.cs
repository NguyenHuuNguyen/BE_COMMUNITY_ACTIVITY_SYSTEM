using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByAccountAsync(string account);
        Task<ICollection<User>> GetUsersAsync();
        Task<ICollection<User>> GetTeachersAsync();
        Task<BasePaginationDto<UserGetDto>> GetTeachersPaginationAsync(BasePaginationRequestDto dto);
        Task<ICollection<UserGetWithCommunityActivityScoreDto>> GetStudentsByClassIdAsync(string classId, int year);
        Task<User> CreateUserAsync(UserCreateDto user);
        Task<User> UpdateUserAsync(UserUpdateDto user);
        Task<User> UpdateUserStatusAsync(string userId, int status);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> CheckUserExistsAsync(string userId);
        Task<string?> UploadAvatarAsync(string userId, IFormFile image);
        bool CheckStudentBelongToClass(string studentId, string classId);
        bool CheckUserExists(string userId);
        bool CheckIdentificationCardIdExists(string identificationCardId);
        bool CheckIdentificationCardIdExists(string id, string identificationCardId);
        bool CheckUserIsStudent(string userId);
    }
}
