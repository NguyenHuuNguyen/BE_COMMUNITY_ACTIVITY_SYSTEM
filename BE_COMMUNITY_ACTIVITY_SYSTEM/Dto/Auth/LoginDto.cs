using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Auth
{
    public class LoginDto
    {
        [Required]
        public string? AccountId { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
