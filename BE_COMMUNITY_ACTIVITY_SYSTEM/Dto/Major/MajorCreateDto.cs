using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major
{
    public class MajorCreateDto
    {
        [Required]
        public String? MajorHeadId { get; set; }
        [Required]
        public String? Name { get; set; }
    }
}
