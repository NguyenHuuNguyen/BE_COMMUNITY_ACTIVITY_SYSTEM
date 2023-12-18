using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType;
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
    public class CommunityActivityTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;
        private readonly ICommonRepository _commonRepository;

        public CommunityActivityTypeController(IMapper mapper, ICommunityActivityTypeRepository communityActivityTypeRepository, ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _communityActivityTypeRepository = communityActivityTypeRepository;
            _commonRepository = commonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommunityActivityTypeGetDto>))]
        public async Task<IActionResult> GetCommunityActivityTypesList()
        {
            var communityActivityType = _mapper.Map<List<CommunityActivityTypeGetDto>>(await _communityActivityTypeRepository.GetCommunityActivityTypeAsync());
            return Ok(communityActivityType);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BasePaginationDto<CommunityActivityTypeGetDto>))]
        public async Task<IActionResult> GetCommunityActivityTypesPaginationList([FromQuery] BasePaginationRequestDto dto)
        {
            var communityActivityType = await _communityActivityTypeRepository.GetCommunityActivityTypesPaginationAsync(dto);
            return Ok(communityActivityType);
        }

        [HttpPost, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(CommunityActivityTypeGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> CreateCommunityActivityType([FromBody] CommunityActivityTypeCreateDto dto)
        {
            var communityActivityType = _mapper.Map<CommunityActivityTypeGetDto>(await _communityActivityTypeRepository.CreateCommunityActivityTypeAsync(dto));
            return Ok(communityActivityType);
        }

        [HttpPut, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(CommunityActivityTypeGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] CommunityActivityTypeUpdateDto dto)
        {
            var communityActivityType = _mapper.Map<CommunityActivityTypeGetDto>(await _communityActivityTypeRepository.UpdateCommunityActivityTypeAsync(dto));
            return Ok(communityActivityType);
        }

        [HttpDelete, Authorize(Roles = ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCommunityActivityType([FromQuery] string communityActivityTypeId)
        {
            if (_commonRepository.IsGuid(communityActivityTypeId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["CommunityActivityTypeId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "CommunityActivityTypeId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _communityActivityTypeRepository.CheckCommunityActivityTypeExistsAsync(communityActivityTypeId) == false)
            {
                return NotFound();
            }

            await _communityActivityTypeRepository.DeleteCommunityActivityTypeAsync(communityActivityTypeId);
            return NoContent();
        }
    }
}
