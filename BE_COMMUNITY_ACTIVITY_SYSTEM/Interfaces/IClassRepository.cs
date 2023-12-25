using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IClassRepository
    {
        Task<Class> GetClassByIdAsync(string classId);
        Task<ICollection<Class>> GetClassesAsync();
        Task<ICollection<Class>> GetClassesByHeadTeacherIdAsync(string headTeacherId);
        Task<BasePaginationDto<ClassGetDto>> GetClassPaginationAsync(ClassPaginationRequestDto dto);
        Task<Class> CreateClassAsync(ClassCreateDto data);
        Task<Class> UpdateClassAsync(ClassUpdateDto data);
        Task<Class> AssignClassPresidentAsync(ClassAssignClassPresidentDto data);
        Task<bool> DeleteClassAsync(string classId);
        Task<bool> CheckClassExistsAsync(string classId);
        Task<bool> CheckHeadTeacherOfClass(string headTeacherId, string classId);
        bool CheckClassExists(string classId);
    }
}
