using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }
        public Task<bool> AssignClassHeadTeacherAsync(ClassAsignPersonDto data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AssignClassPresidentAsync(ClassAsignPersonDto data)
        {
            throw new NotImplementedException();
        }

        public bool CheckClassExists(string classId)
        {
            return _context.Classes.Any(c => classId.Equals(c.Id) && c.IsDeleted == false);
        }

        public Task<bool> CheckClassExistsAsync(string classId)
        {
            return _context.Classes.AnyAsync(c => classId.Equals(c.Id) && c.IsDeleted == false);
        }

        public Task<Class> CreateClassAsync(ClassCreateDto data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClassAsync(string classId)
        {
            throw new NotImplementedException();
        }

        public Task<Class> GetClassByIdAsync(string classId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Class>> GetClassesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Class>> GetClassesByHeadTeacherIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Class> UpdateClassAsync(ClassUpdateDto data)
        {
            throw new NotImplementedException();
        }
    }
}
