using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface ICommunityActivityRepository
    {
        Task<CommunityActivity> GetById(string id);
        Task<ICollection<CommunityActivity>> GetUserCommunityActivitiesAsync(string userId, int year);
        Task<CommunityActivity> CreateCommunityActivityAsync(CommunityActivityCreateDto dto);
        Task<CommunityActivity> UpdateCommunityActivityAsync(CommunityActivityUpdateDto dto);
        Task<bool> DeleteCommunityActivityAsync(string caId);
        Task<bool> CheckCommunityActivityExistAsync(string caId);
        bool CheckCommunityActivityExist(string caId);
    }
}
