using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class CommunityActivityRepository : ICommunityActivityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommunityActivityRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CheckCommunityActivityExist(string caId)
        {
            return _context.CommunityActivities.Any(ca => caId.Equals(ca.Id) && ca.IsDeleted == false);
        }

        public async Task<bool> CheckCommunityActivityExistAsync(string caId)
        {
            return await _context.CommunityActivities.AnyAsync(ca => caId.Equals(ca.Id) && ca.IsDeleted == false);
        }

        public async Task<CommunityActivity> CreateCommunityActivityAsync(CommunityActivityCreateDto dto)
        {
            var communityActivity = _mapper.Map<CommunityActivity>(dto);

            communityActivity.Id = Guid.NewGuid().ToString();
            communityActivity.CreatedAt = DateTime.UtcNow;

            await _context.CommunityActivities.AddAsync(communityActivity);
            await _context.SaveChangesAsync();

            var result = await _context.CommunityActivities
                                        .Include(ca => ca.CommunityActivityType)
                                        .FirstOrDefaultAsync(ca => ca.Id == communityActivity.Id);

            return result!;
        }

        public async Task<bool> DeleteCommunityActivityAsync(string caId)
        {
            var communityActivity = await _context.CommunityActivities.FirstOrDefaultAsync(ca => caId.Equals(ca.Id) && ca.IsDeleted == false);
            
            if (communityActivity == null)
            {
                return false;
            }

            communityActivity.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CommunityActivity> GetById(string id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _context.CommunityActivities.FirstOrDefaultAsync(ca => id.Equals(ca.Id) && ca.IsDeleted == false);
            #pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<ICollection<CommunityActivity>> GetUserCommunityActivitiesAsync(string userId, int year)
        {
            return await _context.CommunityActivities.Include(ca => ca.CommunityActivityType).Where(ca => userId.Equals(ca.UserId) && year == ca.Year && ca.IsDeleted == false).ToListAsync();
        }

        public async Task<CommunityActivity> UpdateCommunityActivityAsync(CommunityActivityUpdateDto dto)
        {
            var communityActivity = await _context.CommunityActivities.FirstOrDefaultAsync(ca => dto.Id!.Equals(ca.Id) && ca.IsDeleted == false);

            if (communityActivity == null)
            {
                return null!;
            }

            _mapper.Map(dto, communityActivity);
            communityActivity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var result = await _context.CommunityActivities
                                        .Include(ca => ca.CommunityActivityType)
                                        .FirstOrDefaultAsync(ca => ca.Id == communityActivity.Id);

            return result!;
        }
    }
}
