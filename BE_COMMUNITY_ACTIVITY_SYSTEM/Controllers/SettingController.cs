using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.NewFolder;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class SettingController : Controller
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public SettingController(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SettingGetDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSettingByName([FromQuery] string name)
        {
            if (await _settingRepository.CheckSettingExistByNameAsync(name) == false)
            {
                return NotFound();
            }

            var setting = _mapper.Map<SettingGetDto>(await _settingRepository.GetSettingByNameAsync(name));
            return Ok(setting);
        }

        [HttpPatch]
        [ProducesResponseType(200, Type = typeof(SettingGetDto))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSettingStatus([FromQuery] string name, [FromQuery] int status)
        {
            if (await _settingRepository.CheckSettingExistByNameAsync(name) == false)
            {
                return NotFound();
            }

            var setting = await _settingRepository.GetSettingByNameAsync(name);
            var result = _mapper.Map<SettingGetDto>(await _settingRepository.UpdateSettingStatusAsync(setting.Id!, status));

            return Ok(result);
        }
    }
}
