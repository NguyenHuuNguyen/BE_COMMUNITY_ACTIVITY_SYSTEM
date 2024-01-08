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

        public async Task<ICollection<CommunityActivityType>> GetCommunityActivityTypesAsync()
        {
            return await _context.CommunityActivityTypes.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<BasePaginationDto<CommunityActivityTypeGetDto>> GetCommunityActivityTypesPaginationAsync(BasePaginationRequestDto dto)
        {
            dto.ValidateInput();

            int totalItems = await _context.CommunityActivityTypes
                .Where(a => a.Name!.ToLower().Contains(dto.Filter!) && a.IsDeleted == false)
                .CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / dto.ItemPerPage);
            dto.Page = totalPages > 0 ? Math.Min(dto.Page, totalPages) : 1;
            int skipCount = (dto.Page - 1) * dto.ItemPerPage;
            bool isNextPage = skipCount + dto.ItemPerPage < totalItems;
            bool isPreviousPage = dto.Page > 1;

            var communityActivityTypes = await _context.CommunityActivityTypes
                .Where(a => a.Name!.ToLower().Contains(dto.Filter!) && a.IsDeleted == false)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(dto.ItemPerPage)
                .ToListAsync();

            var communityActivityTypeDtos = _mapper.Map<List<CommunityActivityTypeGetDto>>(communityActivityTypes);

            var paginationResult = new BasePaginationDto<CommunityActivityTypeGetDto>
            {
                Data = communityActivityTypeDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = dto.ItemPerPage,
                CurrentPage = dto.Page,
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

        public bool CheckCommunityActivityTypeNameExists(string name)
        {
            return _context.CommunityActivityTypes.Any(cat => name.ToLower().Equals(cat.Name!.ToLower()) && cat.IsDeleted == false);
        }

        public bool CheckCommunityActivityTypeNameExists(string id, string name)
        {
            return _context.CommunityActivityTypes.Any(cat => name.ToLower().Equals(cat.Name!.ToLower()) 
                                                                && id.Equals(cat.Id) == false
                                                                && cat.IsDeleted == false);
        }

        public CommunityActivityType GetCommunityActivityTypeById(string id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return _context.CommunityActivityTypes.FirstOrDefault(cat => id.Equals(cat.Id) && cat.IsDeleted == false);
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
