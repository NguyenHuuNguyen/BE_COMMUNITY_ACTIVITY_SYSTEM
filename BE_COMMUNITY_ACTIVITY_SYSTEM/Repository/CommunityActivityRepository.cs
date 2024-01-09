using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;
using Microsoft.EntityFrameworkCore;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants.CommunityActivityStatus;

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

        public async Task<ICollection<CommunityActivity>> GetUserCommunityActivitiesAllTimeAsync(string userId)
        {
            return await _context.CommunityActivities.Include(ca => ca.CommunityActivityType).Where(ca => userId.Equals(ca.UserId) && ca.IsDeleted == false).ToListAsync();
        }

        public async Task<int> GetUserCommunityActivitySumScore(string userId, int year, int status)
        {
            var communityActivities = await _context.CommunityActivities
                .Where(ca => userId.Equals(ca.UserId) && ca.Year == year && ca.Status == status && ca.IsDeleted == false)
                .ToListAsync();

            return communityActivities.Count > 0 ? communityActivities.Sum(ca => ca.ClassPresidentEvaluationScore) : -1;
        }

        public async Task<bool> ApproveClassCommunityActivitiesByHeadTeacherAsync(string classId, int year)
        {
            var usersInClass = await _context.Users.Where(u => u.ClassId == classId && u.IsDeleted == false).ToListAsync();

            foreach (var user in usersInClass)
            {
                var communityActivitiesToUpdate = await _context.CommunityActivities
                    .Where(ca => ca.UserId == user.Id 
                                    && ca.Year == year 
                                    && ca.Status == (int)CLASS_PRESIDENT_CONFIRMED 
                                    && ca.IsDeleted == false)
                    .ToListAsync();

                foreach (var activity in communityActivitiesToUpdate)
                {
                    activity.Status = (int)HEAD_TEACHER_CONFIRMED;
                }
            }

            return await _context.SaveChangesAsync() > 0;
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

        public async Task<bool> ApproveMajorCommunityActivitiesByMajorHeadAsync(string majorId, int year)
        {
            var classesInMajor = await _context.Classes
            .Where(c => c.MajorId == majorId && c.IsDeleted == false)
            .ToListAsync();

            foreach (var classItem in classesInMajor)
            {
                var usersInClass = await _context.Users
                    .Where(u => u.ClassId == classItem.Id && u.IsDeleted == false)
                    .ToListAsync();

                foreach (var user in usersInClass)
                {
                    var communityActivitiesToUpdate = await _context.CommunityActivities
                        .Where(ca => ca.UserId == user.Id
                            && ca.Year == year
                            && ca.Status == (int)HEAD_TEACHER_CONFIRMED
                            && ca.IsDeleted == false)
                        .ToListAsync();

                    foreach (var activity in communityActivitiesToUpdate)
                    {
                        activity.Status = (int)MAJOR_HEAD_CONFIRMED;
                    }
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ApproveUserCommunityActivitiesByHeadTeacherAsync(string userId, int year)
        {
            var communityActivitiesToUpdate = await _context.CommunityActivities
            .Where(ca => ca.UserId == userId
                            && ca.Year == year
                            && ca.Status == (int)CLASS_PRESIDENT_CONFIRMED
                            && ca.IsDeleted == false)
            .ToListAsync();

            foreach (var activity in communityActivitiesToUpdate)
            {
                activity.Status = (int)HEAD_TEACHER_CONFIRMED;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<UserGetWithCommunityActivityScoreDto>> GetUserCommunityActivitiesSumScoreHeadTeachersConfirmedAsync(string majorId, int year)
        {
            var result = new List<UserGetWithCommunityActivityScoreDto>();

            var classesInMajor = await _context.Classes
                .Include(c => c.Users)
                .Where(c => c.MajorId == majorId && c.IsDeleted == false)
                .ToListAsync();

            foreach (var classInMajor in classesInMajor)
            {
                if (classInMajor.Users == null) continue;

                foreach (var user in classInMajor.Users)
                {
                    int sumScoreHeadTeacherConfirmed = await GetUserCommunityActivitySumScore(user.Id!, year, (int)HEAD_TEACHER_CONFIRMED);
                    int sumScoreMajorHeadConfirmed = await GetUserCommunityActivitySumScore(user.Id!, year, (int)MAJOR_HEAD_CONFIRMED);

                    if (sumScoreHeadTeacherConfirmed < 0 && sumScoreMajorHeadConfirmed < 0) continue;

                    var userResult = _mapper.Map<UserGetWithCommunityActivityScoreDto>(user);
                    userResult.SumScoreClassPresidentConfirmed = -1;
                    userResult.SumScoreHeadTeacherConfirmed = sumScoreHeadTeacherConfirmed;
                    userResult.SumScoreMajorHeadConfirmed = sumScoreMajorHeadConfirmed;

                    result.Add(userResult);
                }
            }

            return result.ToList();
        }

        public async Task<ICollection<UserGetWithCommunityActivityScoreDto>> GetUserCommunityActivitiesSumScoreMajorHeadsConfimedAsync(int year)
        {
            var result = new List<UserGetWithCommunityActivityScoreDto>();
            var users = await _context.Users
                .Where(u => u.StudentId != null && u.TeacherId == null && u.IsDeleted == false)
                .OrderBy(u => u.ClassId != null ? u.ClassId : "")
                .ToListAsync();
            
            foreach (var user in users)
            {
                int sumScoreMajorHeadConfirmed = await GetUserCommunityActivitySumScore(user.Id!, year, (int)MAJOR_HEAD_CONFIRMED);

                if (sumScoreMajorHeadConfirmed < 0) continue;

                var userResult = _mapper.Map<UserGetWithCommunityActivityScoreDto>(user);
                userResult.SumScoreClassPresidentConfirmed = -1;
                userResult.SumScoreHeadTeacherConfirmed = -1;
                userResult.SumScoreMajorHeadConfirmed = sumScoreMajorHeadConfirmed;

                result.Add(userResult);
            }

            return result.ToList();
        }
    }
}
