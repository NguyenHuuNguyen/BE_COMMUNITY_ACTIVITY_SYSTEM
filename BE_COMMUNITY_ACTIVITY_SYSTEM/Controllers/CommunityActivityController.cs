using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Repository;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class CommunityActivityController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly ICommunityActivityRepository _communityActivityRepository;
        private readonly ICommunityActivityTypeRepository _communityActivityTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public CommunityActivityController(ICommonRepository commonRepository, ICommunityActivityRepository communityActivityRepository
            , ICommunityActivityTypeRepository communityActivityTypeRepository, IUserRepository userRepository, IMapper mapper, IAuthRepository authRepository)
        {
            _commonRepository = commonRepository;
            _communityActivityRepository = communityActivityRepository;
            _communityActivityTypeRepository = communityActivityTypeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _authRepository = authRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommunityActivityGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserCommunityActivities([FromQuery] string userId, [FromQuery] int year)
        {
            if (_commonRepository.IsGuid(userId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["userId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "UserId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _userRepository.CheckUserExistsAsync(userId) == false)
            {
                return NotFound();
            }

            var communityActivities = _mapper.Map<List<CommunityActivityGetDto>>(await _communityActivityRepository.GetUserCommunityActivitiesAsync(userId, year));
            return Ok(communityActivities);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CommunityActivityGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateCommunityActivity([FromBody] CommunityActivityCreateDto dto)
        {
            var communityActivity = _mapper.Map<CommunityActivityGetDto>(await _communityActivityRepository.CreateCommunityActivityAsync(dto));
            return Ok(communityActivity);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(CommunityActivityGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateCommunityActivity([FromBody] CommunityActivityUpdateDto dto)
        {
            var communityActivity = _mapper.Map<CommunityActivityGetDto>(await _communityActivityRepository.UpdateCommunityActivityAsync(dto));
            return Ok(communityActivity);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCommunityActivity([FromQuery] string caId)
        {
            if (_commonRepository.IsGuid(caId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["caId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "caId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _communityActivityRepository.CheckCommunityActivityExistAsync(caId) == false)
            {
                return NotFound();
            }

            var communityActivity = await _communityActivityRepository.GetById(caId);
            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, communityActivity.UserId!) == false)
            {
                return Forbid();
            }

            await _communityActivityRepository.DeleteCommunityActivityAsync(caId);
            return NoContent();
        }
    }
}
