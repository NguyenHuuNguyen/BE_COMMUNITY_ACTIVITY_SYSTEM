using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class Major
    {
        [Key]
        public String? Id { get; set; }
        // FK1
        public String? MajorHeadId { get; set; }
        public String? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<Class>? Classes { get; set; }
        public User? MajorHead { get; set; }
    }
}
