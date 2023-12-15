using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
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

        public UserRepository(DataContext context, IMapper mapper, ICommonRepository commonRepository)
        {
            _context = context;
            _mapper = mapper;
            _commonRepository = commonRepository;
        }

        public async Task<User> CreateUserAsync(UserCreateDto user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.Id= Guid.NewGuid().ToString();

            byte[] passwordHash, passwordSalt;

            if (user.IsStudent)
            {
                newUser.StudentId = await GetNextStudentId();
                _commonRepository.CreatePasswordHash(newUser.StudentId, out passwordHash, out passwordSalt);
                // Phân quyền sinh viên
            }
            else
            {
                newUser.TeacherId = await GetNextTeacherId();
                _commonRepository.CreatePasswordHash(newUser.TeacherId, out passwordHash, out passwordSalt);
                // Phân quyền giáo viên
            }

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
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

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<ICollection<User>> GetUsersByClassIdAsync(string classId)
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
    }
}
