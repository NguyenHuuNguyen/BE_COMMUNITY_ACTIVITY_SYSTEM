using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class;
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
    public class ClassController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClassRepository _classRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public ClassController(IMapper mapper, IClassRepository classRepository, ICommonRepository commonRepository, IAuthRepository authRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _classRepository = classRepository;
            _commonRepository = commonRepository;
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassGetDto>))]
        public async Task<IActionResult> GetClassesList()
        {
            var classes = _mapper.Map<List<ClassGetDto>>(await _classRepository.GetClassesAsync());
            return Ok(classes);
        }

        [HttpGet, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(BasePaginationDto<ClassGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> GetClassesPaginationList([FromQuery] ClassPaginationRequestDto dto)
        {
            var classes = await _classRepository.GetClassPaginationAsync(dto);
            return Ok(classes);
        }

        [HttpGet, Authorize(Roles = ADMIN + "," + GIAO_VIEN)]
        [ProducesResponseType(200, Type = typeof(ICollection<ClassGetDto>))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetClassesByTeacherId([FromQuery] string teacherId = "")
        {
            if (_commonRepository.IsGuid(teacherId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["userId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "UserId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _userRepository.CheckUserExistsAsync(teacherId) == false)
            {
                return NotFound();
            }

            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, teacherId) == false)
            {
                return Forbid();
            }
            var classes = _mapper.Map<List<ClassGetDto>>(await _classRepository.GetClassesByHeadTeacherIdAsync(teacherId));
            return Ok(classes);
        }

        [HttpPost, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(ClassGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreateDto dto)
        {
            var classes = _mapper.Map<ClassGetDto>(await _classRepository.CreateClassAsync(dto));
            return Ok(classes);
        }

        [HttpPut, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(ClassGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateClass([FromBody] ClassUpdateDto dto)
        {
            var classes = _mapper.Map<ClassGetDto>(await _classRepository.UpdateClassAsync(dto));
            return Ok(classes);
        }

        [HttpPatch, Authorize(Roles = ADMIN + ", " + GIAO_VIEN)]
        [ProducesResponseType(200, Type = typeof(ClassAssignClassPresidentDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateClassPresident([FromBody] ClassAssignClassPresidentDto dto)
        {
            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (!await _classRepository.CheckHeadTeacherOfClass(tokenUserId, dto.ClassId!))
            {
                return Forbid();
            }

            var classes = _mapper.Map<ClassGetDto>(await _classRepository.AssignClassPresidentAsync(dto));
            return Ok(classes);
        }

        [HttpDelete, Authorize(Roles = ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteClass([FromQuery] string classId)
        {
            if (_commonRepository.IsGuid(classId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["ClassId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "ClassId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _classRepository.CheckClassExistsAsync(classId) == false)
            {
                return NotFound();
            }

            await _classRepository.DeleteClassAsync(classId);
            return NoContent();
        }
    }
}
