using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class CommunityActivityTypeRepository : ICommunityActivityTypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommunityActivityTypeRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<bool> CheckCommunityActivityTypeExistsAsync(string id)
        {
            return await _context.CommunityActivityTypes.AnyAsync(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public bool CheckCommunityActivityTypeExists(string id)
        {
            return _context.CommunityActivityTypes.Any(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public async Task<CommunityActivityType> CreateCommunityActivityTypeAsync(CommunityActivityTypeCreateDto dto)
        {
            var communityActivityType = _mapper.Map<CommunityActivityType>(dto);

            communityActivityType.Id = Guid.NewGuid().ToString();
            communityActivityType.CreatedAt = DateTime.UtcNow;

            await _context.CommunityActivityTypes.AddAsync(communityActivityType);
            await _context.SaveChangesAsync();
            return communityActivityType;
        }

        public async Task<bool> DeleteCommunityActivityTypeAsync(string id)
        {
            var communityActivityType = await _context.CommunityActivityTypes.FirstOrDefaultAsync(a => id.Equals(a.Id) && a.IsDeleted == false);
            if (communityActivityType == null)
            {
                return false;
            }
            communityActivityType.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<CommunityActivityType>> GetCommunityActivityTypeAsync()
        {
            return await _context.CommunityActivityTypes.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<BasePaginationDto<CommunityActivityTypeGetDto>> GetCommunityActivityTypesPaginationAsync(BasePaginationRequestDto dto)
        {
            int itemPerPage = dto.ItemPerPage <= 0 ? 10 : dto.ItemPerPage;
            int page = dto.Page <= 0 ? 1 : dto.Page;
            dto.Filter = dto.Filter == null ? String.Empty : dto.Filter.ToLower();

            int totalItems = await _context.CommunityActivityTypes.CountAsync(a => a.IsDeleted == false);
            int totalPages = (int)Math.Ceiling((double)totalItems / itemPerPage);
            page = Math.Min(page, totalPages);
            int skipCount = (page - 1) * itemPerPage;
            bool isNextPage = skipCount + itemPerPage < totalItems;
            bool isPreviousPage = page > 1;

            var communityActivityTypes = await _context.CommunityActivityTypes
                .Where(a => a.Name!.ToLower().Contains(dto.Filter) && a.IsDeleted == false)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(itemPerPage)
                .ToListAsync();

            var communityActivityTypeDtos = _mapper.Map<List<CommunityActivityTypeGetDto>>(communityActivityTypes);

            var paginationResult = new BasePaginationDto<CommunityActivityTypeGetDto>
            {
                Data = communityActivityTypeDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = itemPerPage,
                CurrentPage = page,
                IsNextPage = isNextPage,
                IsPreviousPage = isPreviousPage
            };

            return paginationResult;
        }

        public async Task<CommunityActivityType> UpdateCommunityActivityTypeAsync(CommunityActivityTypeUpdateDto dto)
        {
            var communityActivityType = await _context.CommunityActivityTypes.FirstOrDefaultAsync(a => dto.Id!.Equals(a.Id) && a.IsDeleted == false);
            if (communityActivityType == null)
            {
                return null!;
            }

            _mapper.Map(dto, communityActivityType);
            communityActivityType.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return communityActivityType;
        }
    }
}
