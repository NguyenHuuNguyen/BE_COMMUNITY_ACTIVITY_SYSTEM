using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Major
{
    public class MajorUpdateDto
    {
        [Required]
        public String? Id { get; set; }
        [Required]
        public String? MajorHeadId { get; set; }
        [Required]
        public String? Name { get; set; }
    }
}
