using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Auth;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Models;
using Microsoft.AspNetCore.Authorization;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Ultis;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(LoginSuccessDto))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (await _authRepository.CheckLogin(dto.AccountId!, dto.Password!))
            {
                if(await _authRepository.CheckAccountLockedAsync(dto.AccountId!))
                {
                    return Forbid();
                }

                var user = await _userRepository.GetUserByAccountAsync(dto.AccountId!);
                var token = await _authRepository.CreateToken(user);
                var returnData = new LoginSuccessDto()
                {
                    IsSuccess = true,
                    Token = token,
                };

                return Ok(returnData);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(BaseErrorDto))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var tokenUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (await _authRepository.CheckUserAuthorizedForActionAsync(tokenUserId, dto.UserId!))
            {
                return Forbid();
            }

            await _authRepository.ChangePasswordAsync(dto.UserId!, dto.Password!);
            var user = await _userRepository.GetUserByIdAsync(dto.UserId!);
            var token = _authRepository.CreateToken(user);
            return Ok(token);
        }
    }
}
