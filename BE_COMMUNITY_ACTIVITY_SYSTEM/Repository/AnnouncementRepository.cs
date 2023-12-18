using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Data;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnnouncementRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CheckAnnouncementExists(string id)
        {
            return _context.Announcements.Any(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public async Task<bool> CheckAnnouncementExistsAsync(string id)
        {
            return await _context.Announcements.AnyAsync(a => id.Equals(a.Id) && a.IsDeleted == false);
        }

        public async Task<Announcement> CreateAnnouncementAsync(AnnouncementCreateDto dto)
        {
            var announcement = _mapper.Map<Announcement>(dto);

            announcement.Id = Guid.NewGuid().ToString();
            announcement.CreatedAt = DateTime.Now;

            await _context.Announcements.AddAsync(announcement);
            await _context.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> DeleteAnnouncementAsync(string id)
        {
            var announcement = await _context.Announcements.FirstOrDefaultAsync(a => id.Equals(a.Id) && a.IsDeleted == false);

            if (announcement == null)
            {
                return false;
            }

            announcement.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Announcement>> GetAnnouncementsAsync()
        {
            return await _context.Announcements.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<BasePaginationDto<AnnouncementGetDto>> GetAnnouncementsPaginationAsync(BasePaginationRequestDto dto)
        {
            dto.ValidateInput();

            int totalItems = await _context.Announcements.CountAsync(a => a.IsDeleted == false);
            int totalPages = (int)Math.Ceiling((double)totalItems / dto.ItemPerPage);
            dto.Page = Math.Min(dto.Page, totalPages);
            int skipCount = (dto.Page - 1) * dto.ItemPerPage;
            bool isNextPage = skipCount + dto.ItemPerPage < totalItems;
            bool isPreviousPage = dto.Page > 1;

            var announcements = await _context.Announcements
                .Where(a => (string.Concat(a.Title, a.Content).ToLower().Contains(dto.Filter!)) && a.IsDeleted == false)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipCount)
                .Take(dto.ItemPerPage)
                .ToListAsync();

            var announcementDtos = _mapper.Map<List<AnnouncementGetDto>>(announcements);

            var paginationResult = new BasePaginationDto<AnnouncementGetDto>
            {
                Data = announcementDtos,
                TotalItems = totalItems,
                TotalPages = totalPages,
                ItemPerPage = dto.ItemPerPage,
                CurrentPage = dto.Page,
                IsNextPage = isNextPage,
                IsPreviousPage = isPreviousPage
            };

            return paginationResult;
        }

        public async Task<Announcement> UpdateAnnouncementAsync(AnnouncementUpdateDto dto)
        {
            var announcement = await _context.Announcements.FirstOrDefaultAsync(a => dto.Id!.Equals(a.Id) && a.IsDeleted == false);

            if (announcement == null)
            {
                return null!;
            }

            _mapper.Map(dto, announcement);
            announcement.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return announcement;
        }
    }
}
