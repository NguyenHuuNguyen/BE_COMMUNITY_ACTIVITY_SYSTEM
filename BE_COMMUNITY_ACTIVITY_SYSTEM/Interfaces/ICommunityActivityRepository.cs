using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface ICommunityActivityRepository
    {
        Task<CommunityActivity> GetById(string id);
        Task<ICollection<CommunityActivity>> GetUserCommunityActivitiesAsync(string userId, int year);
        Task<ICollection<CommunityActivity>> GetUserCommunityActivitiesAllTimeAsync(string userId);
        Task<ICollection<UserGetWithCommunityActivityScoreDto>> GetUserCommunityActivitiesSumScoreHeadTeachersConfirmedAsync(string majorId, int year);
        Task<ICollection<UserGetWithCommunityActivityScoreDto>> GetUserCommunityActivitiesSumScoreMajorHeadsConfimedAsync(int year);
        Task<int> GetUserCommunityActivitySumScore(string userId, int year, int status);
        Task<CommunityActivity> CreateCommunityActivityAsync(CommunityActivityCreateDto dto);
        Task<CommunityActivity> UpdateCommunityActivityAsync(CommunityActivityUpdateDto dto);
        Task<bool> ApproveUserCommunityActivitiesByHeadTeacherAsync(string userId, int year);
        Task<bool> ApproveClassCommunityActivitiesByHeadTeacherAsync(string classId, int year);
        Task<bool> ApproveMajorCommunityActivitiesByMajorHeadAsync(string majorId, int year);
        Task<bool> DeleteCommunityActivityAsync(string caId);
        Task<bool> CheckCommunityActivityExistAsync(string caId);
        bool CheckCommunityActivityExist(string caId);
    }
}
