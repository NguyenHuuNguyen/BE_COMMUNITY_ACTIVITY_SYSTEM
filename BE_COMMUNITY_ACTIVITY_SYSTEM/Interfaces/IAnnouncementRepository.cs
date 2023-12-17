using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<ICollection<Announcement>> GetAnnouncementsAsync();
        Task<BasePaginationDto<AnnouncementGetDto>> GetAnnouncementsPaginationAsync(BasePaginationRequestDto dto);
        Task<Announcement> CreateAnnouncementAsync(AnnouncementCreateDto data);
        Task<Announcement> UpdateAnnouncementAsync(AnnouncementUpdateDto data);
        Task<bool> DeleteAnnouncementAsync(string id);
        Task<bool> CheckAnnouncementExistsAsync(string id);
        bool CheckAnnouncementExists(string id);
    }
}

