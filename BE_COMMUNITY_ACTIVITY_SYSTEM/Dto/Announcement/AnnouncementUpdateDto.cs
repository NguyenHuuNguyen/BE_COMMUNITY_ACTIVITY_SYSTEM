using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement
{
    public class AnnouncementUpdateDto
    {
        [Required]
        public String? Id { get; set; }
        [Required]
        public String? Title { get; set; }
        [Required]
        public String? Content { get; set; }
    }
}
