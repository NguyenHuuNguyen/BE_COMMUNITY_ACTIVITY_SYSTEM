using BE_COMMUNITY_ACTIVITY_SYSTEM.Dto;
using BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Repository
{
    public class CommonRepository : ICommonRepository
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public IActionResult CreateBadRequestResponse(ControllerBase controller, Dictionary<string, List<string>> errors)
        {
            var errorResponse = new BaseErrorDto
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = 400,
                TraceId = controller.HttpContext.TraceIdentifier,
                Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
        }

        public bool IsGuid(string input)
        {
            if (input == null) return false;
            return Guid.TryParse(input, out _);
        }
    }
}
