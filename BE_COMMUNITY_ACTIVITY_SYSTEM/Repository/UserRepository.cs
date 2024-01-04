using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICommonRepository _commonRepository;
        private readonly IAuthRepository _authRepository;

        public UserRepository(DataContext context, IMapper mapper, ICommonRepository commonRepository, IAuthRepository authRepository)
        {
            _context = context;
            _mapper = mapper;
            _commonRepository = commonRepository;
            _authRepository = authRepository;
        }

        public async Task<User> CreateUserAsync(UserCreateDto user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Id= Guid.NewGuid().ToString();
            newUser.CreatedAt = DateTime.Now;
            newUser.UpdatedAt = DateTime.Now;

            byte[] passwordHash, passwordSalt;

            if (user.IsStudent)
            {
                newUser.StudentId = await GetNextStudentId();
                _commonRepository.CreatePasswordHash(newUser.StudentId, out passwordHash, out passwordSalt);
            }
            else
            {
                newUser.TeacherId = await GetNextTeacherId();
                _commonRepository.CreatePasswordHash(newUser.TeacherId, out passwordHash, out passwordSalt);
            }

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            if (user.IsStudent)
            {
                await _authRepository.AsignRoleAsync(newUser.Id, Constants.Roles.SINH_VIEN);
            }
            else
            {
                await _authRepository.AsignRoleAsync(newUser.Id, Constants.Roles.GIAO_VIEN);
            }
            return newUser;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => userId.Equals(u.Id) && !u.IsDeleted);
            if (user == null)
            {
                return false;
            }
            user.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => userId.Equals(u.Id) && !u.IsDeleted);
            return user!;
        }

        public async Task<User> GetUserByAccountAsync(string account)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => (account.Equals(u.StudentId) || account.Equals(u.TeacherId)) 
                                                                    && !u.IsDeleted);
            return user!;
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<ICollection<User>> GetTeachersAsync()
        {
            return await _context.Users.Where(u => !u.IsDeleted && u.TeacherId != null && u.StudentId == null).ToListAsync();
        }

        public async Task<BasePaginationDto<UserGetDto>> GetTeachersPaginationAsync(BasePaginationRequestDto dto)
        {
            dto.ValidateInput();

            int totalItems = await _context.Users
                .Where(u => u.IsDeleted == false 
                    && u.TeacherId != null 
                    && u.StudentId == null 
                    && (string.Concat(u.TeacherId, u.FirstName, " ", u.LastName).ToLower().Contains(dto.Filter!)))
                .CountAsync();

            int totalPages = (int)Math.Ceiling((double)totalItems / dto.ItemPerPage);
            dto.Page = totalPages > 0 ? Math.Min(dto.Page, totalPages) : 1;
            int skipCount = (dto.Page - 1) * dto.ItemPerPage;
            bool isNextPage = skipCount + dto.ItemPerPage < totalItems;
            bool isPreviousPage = dto.Page > 1;

            var users = await _context.Users
                .Where(u => u.IsDeleted == false 
                            && u.TeacherId != null 
                            && u.StudentId == null 
                            && (string.Concat(u.TeacherId, u.FirstName, " ", u.LastName).ToLower().Contains(dto.Filter!)))
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(dto.ItemPerPage)
                .ToListAsync();

            var userDtos = _mapper.Map<List<UserGetDto>>(users);

            var paginationResult = new BasePaginationDto<UserGetDto>
            {
                Data = userDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = dto.ItemPerPage,
                CurrentPage = dto.Page,
                IsNextPage = isNextPage,
                IsPreviousPage = isPreviousPage
            };

            return paginationResult;
        }

        public async Task<ICollection<User>> GetStudentsByClassIdAsync(string classId)
        {
            return await _context.Users.Where(u => !u.IsDeleted && classId.Equals(u.ClassId)).ToListAsync();
        }

        public async Task<User> UpdateUserStatusAsync(string userId, int status)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => userId.Equals(u.Id) && !u.IsDeleted);
            if (user == null)
            {
                return null!;
            }
            user.Status = status;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(UserUpdateDto userUpdate)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(cu => userUpdate.Id!.Equals(cu.Id) && !cu.IsDeleted);

            if (currentUser == null)
            {
                return null!;
            }

            _mapper.Map(userUpdate, currentUser);
            currentUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return currentUser;
        }

        public async Task<bool> CheckUserExistsAsync(string userId)
        {
            return await _context.Users.AnyAsync(u => userId.Equals(u.Id) && u.IsDeleted == false);
        }

        public bool CheckUserExists(string userId)
        {
            return _context.Users.Any(u => userId.Equals(u.Id) && u.IsDeleted == false);
        }

        public bool CheckUserIsStudent(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => userId.Equals(u.Id));
            if (user == null)
            {
                return false;
            }
            return user.StudentId != null;
        }

        public bool CheckStudentBelongToClass(string id, string classId)
        {
            var user = _context.Users.FirstOrDefault(u => id.Equals(u.Id) && classId.Equals(u.ClassId) && u.StudentId != null);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string?> UploadAvatarAsync(string userId, IFormFile image)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => userId.Equals(u.Id) && !u.IsDeleted);

            if (user == null)
            {
                return null;
            }

            var fileName = $"{userId}.png";
            var filePath = Path.Combine(Constants.Users.AVATAR_PREFIX, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return filePath;
        }


        private async Task<string> GetNextStudentId()
        {
            var lastStudent = await _context.Users
                .Where(u => u.StudentId != null && u.StudentId.StartsWith(Constants.Users.STUDENT_ID_PREFIX))
                .OrderByDescending(u => u.StudentId)
                .FirstOrDefaultAsync();

            var nextNumber = 1;
            if (lastStudent != null)
            {
                var lastNumberStr = lastStudent.StudentId?[Constants.Users.STUDENT_ID_PREFIX.Length..];
                if (int.TryParse(lastNumberStr, out var lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var nextNumberStr = nextNumber.ToString().PadLeft(8, '0');
            var nextStudentId = $"{Constants.Users.STUDENT_ID_PREFIX}{nextNumberStr}";

            return nextStudentId;
        }

        private async Task<string> GetNextTeacherId()
        {
            var lastTeacher = await _context.Users
                .Where(u => u.TeacherId != null && u.TeacherId.StartsWith(Constants.Users.TEACHER_ID_PREFIX))
                .OrderByDescending(u => u.TeacherId)
                .FirstOrDefaultAsync();

            var nextNumber = 1;
            if (lastTeacher != null)
            {
                var lastNumberStr = lastTeacher.TeacherId?[Constants.Users.TEACHER_ID_PREFIX.Length..];
                if (int.TryParse(lastNumberStr, out var lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            var nextNumberStr = nextNumber.ToString().PadLeft(8, '0');
            var nextTeacherId = $"{Constants.Users.TEACHER_ID_PREFIX}{nextNumberStr}";

            return nextTeacherId;
        }

        public bool CheckIdentificationCardIdExists(string identificationCardId)
        {
            return _context.Users.Any(u => identificationCardId.Equals(u.IdentificationCardId) && u.IsDeleted == false);
        }

        public bool CheckIdentificationCardIdExists(string id, string identificationCardId)
        {
            return _context.Users.Any(u => identificationCardId.Equals(u.IdentificationCardId)
                                            && id.Equals(u.Id) == false
                                            && u.IsDeleted == false);
        }
    }
}
