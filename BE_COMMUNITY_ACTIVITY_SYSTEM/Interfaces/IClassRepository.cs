using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IClassRepository
    {
        Task<Class> GetClassByIdAsync(string classId);
        Task<ICollection<Class>> GetClassesAsync();
        Task<ICollection<Class>> GetClassesByHeadTeacherIdAsync();
        Task<Class> CreateClassAsync(ClassCreateDto data);
        Task<Class> UpdateClassAsync(ClassUpdateDto data);
        Task<bool> AssignClassPresidentAsync(ClassAsignPersonDto data);
        Task<bool> AssignClassHeadTeacherAsync(ClassAsignPersonDto data);
        Task<bool> DeleteClassAsync(string classId);
        Task<bool> CheckClassExistsAsync(string classId);
        bool CheckClassExists(string classId);
    }
}
