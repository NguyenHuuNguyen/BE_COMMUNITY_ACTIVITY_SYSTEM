using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major;
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
    public class MajorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMajorRepository _majorRepository;
        private readonly ICommonRepository _commonRepository;

        public MajorController(IMapper mapper, IMajorRepository majorRepository, ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _majorRepository = majorRepository;
            _commonRepository = commonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MajorGetDto>))]
        public async Task<IActionResult> GetMajorsList()
        {
            var majors = _mapper.Map<List<MajorGetDto>>(await _majorRepository.GetMajorsAsync());
            return Ok(majors);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BasePaginationDto<MajorGetDto>))]
        public async Task<IActionResult> GetMajorsPaginationList([FromQuery] BasePaginationRequestDto dto)
        {
            var majors = await _majorRepository.GetMajorsPaginationAsync(dto);
            return Ok(majors);
        }

        [HttpPost, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(MajorGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> CreateMajor([FromBody] MajorCreateDto dto)
        {
            var major = _mapper.Map<MajorGetDto>(await _majorRepository.CreateMajorAsync(dto));
            return Ok(major);
        }

        [HttpPut, Authorize(Roles = ADMIN)]
        [ProducesResponseType(200, Type = typeof(MajorGetDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> UpdateMajor([FromBody] MajorUpdateDto dto)
        {
            var major = _mapper.Map<MajorGetDto>(await _majorRepository.UpdateMajorAsync(dto));
            return Ok(major);
        }

        [HttpDelete, Authorize(Roles = ADMIN)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMajor([FromQuery] string majorId)
        {
            if (_commonRepository.IsGuid(majorId) == false)
            {
                var errors = new Dictionary<string, List<string>>
                {
                    ["MajorId"] = new List<string> { string.Format(ErrorMessages.INVALID_GUID, "MajorId") }
                };

                return _commonRepository.CreateBadRequestResponse(this, errors);
            }

            if (await _majorRepository.CheckMajorExistsAsync(majorId) == false)
            {
                return NotFound();
            }

            await _majorRepository.DeleteMajorAsync(majorId);
            return NoContent();
        }
    }
}
