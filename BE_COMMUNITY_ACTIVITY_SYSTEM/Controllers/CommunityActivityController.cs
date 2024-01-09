using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity;
using AutoMapper;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants.Roles;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;

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
        private readonly IClassRepository _classRepository;
        private readonly IMajorRepository _majorRepository;

        public CommunityActivityController(ICommonRepository commonRepository, ICommunityActivityRepository communityActivityRepository
            , ICommunityActivityTypeRepository communityActivityTypeRepository, IUserRepository userRepository
            , IMapper mapper, IAuthRepository authRepository, IClassRepository classRepository, IMajorRepository majorRepository)
        {
            _commonRepository = commonRepository;
            _communityActivityRepository = communityActivityRepository;
            _communityActivityTypeRepository = communityActivityTypeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _authRepository = authRepository;
            _classRepository = classRepository;
            _majorRepository = majorRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommunityActivityGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserCommunityActivities([FromQuery] string userId, [FromQuery] int year = 0)
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

            List<CommunityActivityGetDto> communityActivities;

            if (year > 0)
            {
                communityActivities = _mapper.Map<List<CommunityActivityGetDto>>(await _communityActivityRepository.GetUserCommunityActivitiesAsync(userId, year));
            }
            else
            {
                communityActivities = _mapper.Map<List<CommunityActivityGetDto>>(await _communityActivityRepository.GetUserCommunityActivitiesAllTimeAsync(userId));
            }

            return Ok(communityActivities);
        }

        [HttpGet, Authorize(Roles = TRUONG_KHOA + "," + ADMIN)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetWithCommunityActivityScoreDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserCommunityActivitiesSumScoreHeadTeachersConfirmed([FromQuery] string majorId, int year)
        {
            if (_commonRepository.IsGuid(majorId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["majorId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "majorId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _majorRepository.CheckMajorExistsAsync(majorId) == false)
            {
                return NotFound();
            }

            var user = await _communityActivityRepository.GetUserCommunityActivitiesSumScoreHeadTeachersConfirmedAsync(majorId, year);
            return Ok(user);
        }

        [HttpGet, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetWithCommunityActivityScoreDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetUserCommunityActivitiesSumScoreMajorHeadsConfimed([FromQuery] int year)
        {
            var user = await _communityActivityRepository.GetUserCommunityActivitiesSumScoreMajorHeadsConfimedAsync(year);
            return Ok(user);
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

        [HttpPatch, Authorize(Roles = GIAO_VIEN + "," + ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ApproveUserCommunityActivitiesByHeadTeacherAsync([FromQuery] string userId, [FromQuery] int year)
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

            await _communityActivityRepository.ApproveUserCommunityActivitiesByHeadTeacherAsync(userId, year);
            return NoContent();
        }

        [HttpPatch, Authorize(Roles = GIAO_VIEN + "," + ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ApproveClassCommunityActivitiesByHeadTeacherAsync([FromQuery] string classId, [FromQuery] int year)
        {
            if (_commonRepository.IsGuid(classId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["classId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "ClassId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _classRepository.CheckClassExistsAsync(classId) == false)
            {
                return NotFound();
            }

            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _classRepository.CheckIsHeadTeacherOfClass(tokenUserId, classId) == false) {
                return Unauthorized();
            }

            await _communityActivityRepository.ApproveClassCommunityActivitiesByHeadTeacherAsync(classId, year);
            return NoContent();
        }

        [HttpPatch, Authorize(Roles = TRUONG_KHOA + "," + ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ApproveMajorCommunityActivitiesByMajorHeadAsync([FromQuery] string majorId, [FromQuery] int year)
        {
            if (_commonRepository.IsGuid(majorId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["majorId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "majorId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _majorRepository.CheckMajorExistsAsync(majorId) == false)
            {
                return NotFound();
            }

            await _communityActivityRepository.ApproveMajorCommunityActivitiesByMajorHeadAsync(majorId, year);
            return NoContent();
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
