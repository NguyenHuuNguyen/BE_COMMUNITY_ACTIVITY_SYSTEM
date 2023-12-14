using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class Announcement
    {
        [Key]
        public String? Id { get; set; }
        public String? Content { get; set; }
        public String? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
