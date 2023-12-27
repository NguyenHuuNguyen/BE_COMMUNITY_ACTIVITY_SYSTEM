using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserUploadAvatarDto
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
