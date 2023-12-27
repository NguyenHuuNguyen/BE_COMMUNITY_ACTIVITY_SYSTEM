using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants.Roles;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommonRepository _commonRepository;
        private readonly IClassRepository _classRepository;
        private readonly IAuthRepository _authRepository;

        public UserController(IUserRepository userRepository, IMapper mapper, ICommonRepository commonRepository, IClassRepository classRepository, IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _commonRepository = commonRepository;
            _classRepository = classRepository;
            _authRepository = authRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserByUserId([FromQuery] string userId)
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

            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, userId) == false)
            {
                return Forbid();
            }

            var user = _mapper.Map<UserGetDto>(await _userRepository.GetUserByIdAsync(userId));
            return Ok(user);
        }

        [HttpGet, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetDto>))]
        public async Task<IActionResult> GetUsersList()
        {
            var users = _mapper.Map<List<UserGetDto>>(await _userRepository.GetUsersAsync());
            return Ok(users);
        }

        [HttpGet, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetDto>))]
        public async Task<IActionResult> GetTeachersList()
        {
            var users = _mapper.Map<List<UserGetDto>>(await _userRepository.GetTeachersAsync());
            return Ok(users);
        }

        [HttpGet, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(BasePaginationDto<UserGetDto>))]
        public async Task<IActionResult> GetTeachersPaginationList([FromQuery] BasePaginationRequestDto dto)
        {
            var users = await _userRepository.GetTeachersPaginationAsync(dto);
            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStudentsListByClassId([FromQuery] string classId)
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

            var users = _mapper.Map<List<UserGetDto>>(await _userRepository.GetStudentsByClassIdAsync(classId));
            return Ok(users);
        }

        [HttpDelete, Authorize(Roles = ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser([FromQuery] string userId)
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

            await _userRepository.DeleteUserAsync(userId);
            return NoContent();
        }

        [HttpPost, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var user = _mapper.Map<UserGetDto>(await _userRepository.CreateUserAsync(userCreate));
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDto userUpdate)
        {
            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, userUpdate.Id!) == false)
            {
                return Forbid();
            }

            
            if (userUpdate.Avatar != null && userUpdate.Avatar.Length > 0)
            {
                await _userRepository.UploadAvatarAsync(userUpdate.Id!, userUpdate.Avatar);
            }

            var user = _mapper.Map<UserGetDto>(await _userRepository.UpdateUserAsync(userUpdate));
            return Ok(user);
        }

        [HttpPatch, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UserStatusUpdateDto updateData)
        {
            var user = _mapper.Map<UserGetDto>(await _userRepository.UpdateUserStatusAsync(updateData.UserId!, updateData.Status));
            return Ok(user);
        }

        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public IActionResult GetAvatar([FromQuery] string userId)
        {
            if (_commonRepository.IsGuid(userId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["userId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "UserId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), Users.AVATAR_PREFIX, $"{userId}.png");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "image/png");
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UploadAvatar([FromForm] UserUploadAvatarDto dto)
        {
            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, dto.UserId!) == false)
            {
                return Forbid();
            }

            var filePath = await _userRepository.UploadAvatarAsync(dto.UserId!, dto.Image!);
            return Ok(filePath);
        }
    }
}
