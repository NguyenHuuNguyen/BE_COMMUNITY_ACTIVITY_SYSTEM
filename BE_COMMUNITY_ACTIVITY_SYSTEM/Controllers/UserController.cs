using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;
using Microsoft.AspNetCore.Mvc;
using static BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis.Constants;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommonRepository _commonRepository;
        private readonly IClassRepository _classRepository;

        public UserController(IUserRepository userRepository, IMapper mapper, ICommonRepository commonRepository, IClassRepository classRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _commonRepository = commonRepository;
            _classRepository = classRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByUserId([FromQuery] string userId)
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

            var user = _mapper.Map<UserGetDto>(await _userRepository.GetUserByIdAsync(userId));
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetDto>))]
        public async Task<IActionResult> GetList()
        {
            var users = _mapper.Map<List<UserGetDto>>(await _userRepository.GetUsersAsync());
            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetListByClassId([FromQuery] string classId)
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

            var users = _mapper.Map<List<UserGetDto>>(await _userRepository.GetUsersByClassIdAsync(classId));
            return Ok(users);
        }

        [HttpDelete]
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

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreate)
        {
            var user = _mapper.Map<UserGetDto>(await _userRepository.CreateUserAsync(userCreate));
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdate)
        {
            var user = _mapper.Map<UserGetDto>(await _userRepository.UpdateUserAsync(userUpdate));
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UserGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UserStatusUpdateDto updateData)
        {
            var user = _mapper.Map<UserGetDto>(await _userRepository.UpdateUserStatusAsync(updateData.UserId!, updateData.Status));
            return Ok(user);
        }
    }
}
