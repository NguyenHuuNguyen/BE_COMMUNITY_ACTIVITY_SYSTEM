﻿using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface ICommunityActivityTypeRepository
    {
        Task<ICollection<CommunityActivityType>> GetCommunityActivityTypesAsync();
        CommunityActivityType GetCommunityActivityTypeById(string id);
        Task<BasePaginationDto<CommunityActivityTypeGetDto>> GetCommunityActivityTypesPaginationAsync(BasePaginationRequestDto dto);
        Task<CommunityActivityType> CreateCommunityActivityTypeAsync(CommunityActivityTypeCreateDto dto);
        Task<CommunityActivityType> UpdateCommunityActivityTypeAsync(CommunityActivityTypeUpdateDto dto);
        Task<bool> DeleteCommunityActivityTypeAsync(string id);
        Task<bool> CheckCommunityActivityTypeExistsAsync(string id);
        bool CheckCommunityActivityTypeExists(string id);
        bool CheckCommunityActivityTypeNameExists(string name);
        bool CheckCommunityActivityTypeNameExists(string id, string name);
    }
}
