using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface IMajorRepository
    {
        Task<ICollection<Major>> GetMajorsAsync();
        Task<BasePaginationDto<MajorGetDto>> GetMajorsPaginationAsync(BasePaginationRequestDto dto);
        Task<Major> CreateMajorAsync(MajorCreateDto data);
        Task<Major> UpdateMajorAsync(MajorUpdateDto data);
        Task<bool> DeleteMajorAsync(string id);
        Task<bool> CheckMajorExistsAsync(string id);
        Task<bool> CheckMajorHeadExistsAsync(string majorHeadId,string id);
        bool CheckMajorExist(string id);
    }
}
