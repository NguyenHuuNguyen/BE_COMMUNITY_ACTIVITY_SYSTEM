using Microsoft.AspNetCore.Mvc;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Interfaces
{
    public interface ICommonRepository
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        IActionResult CreateBadRequestResponse(ControllerBase controller, Dictionary<string, List<string>> errors);
        public bool IsGuid(string input);
    }
}
