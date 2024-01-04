using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class MajorRepository : IMajorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public MajorRepository(DataContext context, IMapper mapper, IAuthRepository authRepository)
        {
            _context = context;
            _mapper = mapper;
            _authRepository = authRepository;
        }

        public bool CheckMajorExist(string id)
        {
            return _context.Majors.Any(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public async Task<bool> CheckMajorExistsAsync(string id)
        {
            return await _context.Majors.AnyAsync(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public async Task<bool> CheckMajorHeadExistsAsync(string majorHeadId, string id)
        {
            return await _context.Majors.AnyAsync(a => majorHeadId.Equals(a.MajorHeadId) && a.IsDeleted == false && a.Id != id);
        }

        public bool CheckMajorNameExists(string majorName)
        {
            return _context.Majors.Any(m => majorName.ToLower().Equals(m.Name!.ToLower()) && m.IsDeleted == false);
        }

        public bool CheckMajorNameExists(string id, string majorName)
        {
            return _context.Majors.Any(m => majorName.ToLower().Equals(m.Name!.ToLower())
                                            && id.Equals(m.Id) == false
                                            && m.IsDeleted == false);
        }

        public async Task<Major> CreateMajorAsync(MajorCreateDto dto)
        {
            var major = _mapper.Map<Major>(dto);

            major.Id = Guid.NewGuid().ToString();
            major.CreatedAt = DateTime.Now;
            await _authRepository.AsignRoleAsync(dto.MajorHeadId!, Constants.Roles.TRUONG_KHOA);
            await _context.Majors.AddAsync(major);
            await _context.SaveChangesAsync();
            return major;
        }

        public async Task<bool> DeleteMajorAsync(string id)
        {
            var major = await _context.Majors.FirstOrDefaultAsync(a => id.Equals(a.Id) && a.IsDeleted == false);

            if (major == null)
            {
                return false;
            }
            major.IsDeleted = true;

            var check = await this.CheckMajorHeadExistsAsync(major.MajorHeadId!, id);
            if (check == false)
            {
                await _authRepository.RevokeRoleAsync(major.MajorHeadId!, Constants.Roles.TRUONG_KHOA);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Major>> GetMajorsAsync()
        {
            return await _context.Majors.Include(a => a.MajorHead).Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<BasePaginationDto<MajorGetDto>> GetMajorsPaginationAsync(BasePaginationRequestDto dto)
        {
            dto.ValidateInput();
            int totalItems = await _context.Majors
                .Where(a => a.Name!.ToLower().Contains(dto.Filter!) && a.IsDeleted == false)
                .CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / dto.ItemPerPage);
            dto.Page = totalPages > 0 ? Math.Min(dto.Page, totalPages) : 1;
            int skipCount = (dto.Page - 1) * dto.ItemPerPage;
            bool isNextPage = skipCount + dto.ItemPerPage < totalItems;
            bool isPreviousPage = dto.Page > 1;

            var majors = await _context.Majors
                .Include(a => a.MajorHead)
                .Where(a => a.Name!.ToLower().Contains(dto.Filter!) && a.IsDeleted == false)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(dto.ItemPerPage)
                .ToListAsync();

            var majorDtos = _mapper.Map<List<MajorGetDto>>(majors);

            var paginationResult = new BasePaginationDto<MajorGetDto>
            {
                Data = majorDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = dto.ItemPerPage,
                CurrentPage = dto.Page,
                IsNextPage = isNextPage,
                IsPreviousPage = isPreviousPage
            };

            return paginationResult;
        }

        public async Task<Major> UpdateMajorAsync(MajorUpdateDto dto)
        {
            var major = await _context.Majors.Include(major => major.MajorHead).FirstOrDefaultAsync(a => dto.Id!.Equals(a.Id) && a.IsDeleted == false);

            if (major == null)
            {
                return null!;
            }

            if (!major.MajorHeadId!.Equals(dto.MajorHeadId))
            {
                var check = await this.CheckMajorHeadExistsAsync(major.MajorHeadId!, major.Id!);
                if (check == false)
                {
                    await _authRepository.RevokeRoleAsync(major.MajorHeadId!, Constants.Roles.TRUONG_KHOA);
                }
            }

            _mapper.Map(dto, major);
            major.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return major;
        }
    }
}
