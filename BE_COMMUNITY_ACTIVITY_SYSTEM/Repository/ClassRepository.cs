using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public ClassRepository(DataContext context, IMapper mapper, IAuthRepository authRepository)
        {
            _context = context;
            _mapper = mapper;
            _authRepository = authRepository;
        }

        public async Task<Class> AssignClassPresidentAsync(ClassAssignClassPresidentDto dto)
        {
            var classes = await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .FirstOrDefaultAsync(a => dto.ClassId!.Equals(a.Id) && a.IsDeleted == false);

            if (classes == null)
            {
                return null!;
            }
            if (classes.ClassPresidentId == null)
            {
                classes.ClassPresidentId = dto.ClassPresidentId;
                await _authRepository.AsignRoleAsync(dto.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
                classes.UpdatedAt = DateTime.Now;
            }
            if (classes.ClassPresidentId != null && classes.ClassPresidentId != dto.ClassPresidentId)
            {
                await _authRepository.RevokeRoleAsync(classes.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
                classes.ClassPresidentId = dto.ClassPresidentId;
                await _authRepository.AsignRoleAsync(dto.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
                classes.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return classes;
        }

        public bool CheckClassExists(string classId)
        {
            return _context.Classes.Any(c => classId.Equals(c.Id) && c.IsDeleted == false);
        }

        public async Task<bool> CheckClassExistsAsync(string classId)
        {
            return await _context.Classes.AnyAsync(c => classId.Equals(c.Id) && c.IsDeleted == false);
        }

        public bool CheckClassNameExists(string className)
        {
            return _context.Classes.Any(c => c.Name!.ToLower().Equals(className.ToLower()) && c.IsDeleted == false);
        }

        public bool CheckClassNameExists(string id, string className)
        {
            return _context.Classes.Any(c => c.Name!.ToLower().Equals(className.ToLower()) 
                                            && c.Id!.Equals(id) == false 
                                            && c.IsDeleted == false);
        }

        public async Task<bool> CheckHeadTeacherOfClass(string headTeacherId, string classId)
        {
            var isAdmin = await _authRepository.CheckUserHasRoleAsync(headTeacherId, Constants.Roles.ADMIN);

            var classes = await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .FirstOrDefaultAsync(a => classId.Equals(a.Id) && a.IsDeleted == false);
            return headTeacherId.Equals(classes!.HeadTeacherId) || isAdmin;
        }

        public async Task<Class> CreateClassAsync(ClassCreateDto dto)
        {
            var classes = _mapper.Map<Class>(dto);

            classes.Id = Guid.NewGuid().ToString();
            classes.CreatedAt = DateTime.Now;
            await _context.Classes.AddAsync(classes);
            await _context.SaveChangesAsync();
            return classes;
        }

        public async Task<bool> DeleteClassAsync(string id)
        {
            var classes = await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .FirstOrDefaultAsync(a => id.Equals(a.Id) && a.IsDeleted == false);

            if (classes == null)
            {
                return false;
            }
            classes.IsDeleted = true;
            await _authRepository.RevokeRoleAsync(classes.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<Class> GetClassByIdAsync(string id)
        {
            var classes = await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .FirstOrDefaultAsync(a => id.Equals(a.Id) && a.IsDeleted == false);
            if (classes == null)
            {
                return null!;
            }
            return classes;
        }

        public async Task<ICollection<Class>> GetClassesAsync()
        {
            return await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<ICollection<Class>> GetClassesByHeadTeacherIdAsync(string id)
        {
            return await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .Where(a => id.Equals(a.HeadTeacherId) && a.IsDeleted == false).ToArrayAsync();
        }

        public async Task<BasePaginationDto<ClassGetDto>> GetClassPaginationAsync(ClassPaginationRequestDto dto)
        {
            dto.ValidateInput();
            int totalItems = await _context.Classes
                .CountAsync(c => (dto.AcademyYear <= 0 || c.AcademicYear == dto.AcademyYear) &&
                                 c.Major!.Id!.Contains(dto.MajorId!) &&
                                 c.IsDeleted == false);
            int totalPages = (int)Math.Ceiling((double)totalItems / dto.ItemPerPage);
            dto.Page = totalPages > 0 ? Math.Min(dto.Page, totalPages) : 1;
            int skipCount = (dto.Page - 1) * dto.ItemPerPage;
            bool isNextPage = skipCount + dto.ItemPerPage < totalItems;
            bool isPreviousPage = dto.Page > 1;

            var classes = await _context.Classes
                .Include(c => c.Major)
                .Include(c => c.HeadTeacher)
                .Include(c => c.ClassPresident)
                .Where(c => (dto.AcademyYear <= 0 || c.AcademicYear == dto.AcademyYear) &&
                            c.Major!.Id!.Contains(dto.MajorId!) &&
                            c.IsDeleted == false)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(dto.ItemPerPage)
                .ToListAsync();

            var classDtos = _mapper.Map<List<ClassGetDto>>(classes);

            var paginationResult = new BasePaginationDto<ClassGetDto>
            {
                Data = classDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = dto.ItemPerPage,
                CurrentPage = dto.Page,
                IsNextPage = isNextPage,
                IsPreviousPage = isPreviousPage
            };

            return paginationResult;
        }

        public async Task<Class> UpdateClassAsync(ClassUpdateDto dto)
        {
            var classes = await _context.Classes
               .Include(c => c.Major)
               .Include(c => c.HeadTeacher)
               .Include(c => c.ClassPresident)
               .FirstOrDefaultAsync(a => dto.Id!.Equals(a.Id) && a.IsDeleted == false);

            if (classes == null)
            {
                return null!;
            }

            if (classes.ClassPresidentId != null && classes.ClassPresidentId != dto.ClassPresidentId)
            {
                await _authRepository.RevokeRoleAsync(classes.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
                await _authRepository.AsignRoleAsync(dto.ClassPresidentId!, Constants.Roles.LOP_TRUONG);
            }

            _mapper.Map(dto, classes);
            classes.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return classes;
        }
    }
}
