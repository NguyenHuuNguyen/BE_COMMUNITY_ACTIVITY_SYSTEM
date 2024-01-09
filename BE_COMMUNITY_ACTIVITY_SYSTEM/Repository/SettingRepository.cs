using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly DataContext _context;

        public SettingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckSettingExistByNameAsync(string settingName)
        {
            return await _context.Settings.AnyAsync(s => settingName.Equals(s.Name) && s.IsDeleted == false);
        }

        public async Task<Setting> GetSettingByNameAsync(string settingName)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _context.Settings.FirstOrDefaultAsync(s => settingName.Equals(s.Name) && s.IsDeleted == false);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Setting> UpdateSettingStatusAsync(string settingId, int status)
        {
            var setting = await _context.Settings.FirstOrDefaultAsync(s => settingId.Equals(s.Id) && s.IsDeleted == false);

            if (setting == null)
            {
                return null!;
            }

            setting.Status = status;
            return setting;
        }
    }
}
