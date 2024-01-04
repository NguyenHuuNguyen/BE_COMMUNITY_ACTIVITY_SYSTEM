using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class
{
    public class ClassAssignClassPresidentDto
    {
        [Required]
        public String? ClassId { get; set; }
        [Required]
        public String? ClassPresidentId { get; set; }
    }
}
