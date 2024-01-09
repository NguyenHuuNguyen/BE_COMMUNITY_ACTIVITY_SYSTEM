using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface ISettingRepository
    {
        Task<Setting> GetSettingByNameAsync(string settingName);
        Task<Setting> UpdateSettingStatusAsync(string settingId, int status);
        Task<bool> CheckSettingExistByNameAsync(string settingName);
    }
}
