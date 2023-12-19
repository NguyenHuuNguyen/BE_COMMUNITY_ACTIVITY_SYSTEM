using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserCreateDto
    {
        public string? ClassId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string? IdentificationCardId { get; set; }
        [Required]
        public bool IsStudent { get; set; }
    }
}
