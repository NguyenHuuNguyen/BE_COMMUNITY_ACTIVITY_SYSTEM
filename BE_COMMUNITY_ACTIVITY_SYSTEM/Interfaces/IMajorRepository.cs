using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IMajorRepository
    {
        Task<ICollection<Major>> GetMajorsAsync();
        Task<ICollection<Major>> GetMajorsByMajorHeadIdAsync(string majorHeadId);
        Task<BasePaginationDto<MajorGetDto>> GetMajorsPaginationAsync(BasePaginationRequestDto dto);
        Task<Major> CreateMajorAsync(MajorCreateDto data);
        Task<Major> UpdateMajorAsync(MajorUpdateDto data);
        Task<bool> DeleteMajorAsync(string id);
        Task<bool> CheckMajorExistsAsync(string id);
        Task<bool> CheckIsMajorHeadOfOthersMajorAsync(string majorHeadId,string currentMajorId);
        bool CheckMajorExist(string id);
        bool CheckMajorNameExists(string majorName);
        bool CheckMajorNameExists(string id, string majorName);
    }
}
