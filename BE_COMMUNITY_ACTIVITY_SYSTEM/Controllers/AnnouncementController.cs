using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants.Roles;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AnnouncementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICommonRepository _commonRepository;

        public AnnouncementController(IMapper mapper, IAnnouncementRepository announcementRepository, ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _announcementRepository = announcementRepository;
            _commonRepository = commonRepository;
        }

        [HttpGet, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AnnouncementGetDto>))]
        public async Task<IActionResult> GetAnnouncementsList()
        {
            var announcements = _mapper.Map<List<AnnouncementGetDto>>(await _announcementRepository.GetAnnouncementsAsync());
            return Ok(announcements);
        }

        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(BasePaginationDto<AnnouncementGetDto>))]
        public async Task<IActionResult> GetAnnouncementsPaginationList([FromQuery] BasePaginationRequestDto dto)
        {
            var announcements = await _announcementRepository.GetAnnouncementsPaginationAsync(dto);
            return Ok(announcements);
        }

        [HttpPost, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(AnnouncementGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementCreateDto dto)
        {
            var announcement = _mapper.Map<AnnouncementGetDto>(await _announcementRepository.CreateAnnouncementAsync(dto));
            return Ok(announcement);
        }

        [HttpPut, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(AnnouncementGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] AnnouncementUpdateDto dto)
        {
            var announcement = _mapper.Map<AnnouncementGetDto>(await _announcementRepository.UpdateAnnouncementAsync(dto));
            return Ok(announcement);
        }

        [HttpDelete, Authorize(Roles = ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAnnouncement([FromQuery] string announcementId)
        {
            if (_commonRepository.IsGuid(announcementId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["AnnouncementId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "AnnouncementId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _announcementRepository.CheckAnnouncementExistsAsync(announcementId) == false)
            {
                return NotFound();
            }

            await _announcementRepository.DeleteAnnouncementAsync(announcementId);
            return NoContent();
        }
    }
}
