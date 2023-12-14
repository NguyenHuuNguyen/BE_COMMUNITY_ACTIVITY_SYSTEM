using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserCreateDto
    {
        public string? ClassId { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? Nationality { get; set; }
        [Required]
        public string? IdentificationCardId { get; set; }
        public DateTime IdentificationCardIssueDate { get; set; }
        public string? IdentificationCardIssuePlace { get; set; }
        [Required]
        public bool IsStudent { get; set; }
    }
}
