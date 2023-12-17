using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement
{
    public class AnnouncementCreateDto
    {
        [Required]
        public String? Title { get; set; }
        [Required]
        public String? Content { get; set; }
    }
}
