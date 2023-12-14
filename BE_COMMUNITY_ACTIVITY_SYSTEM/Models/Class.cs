using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class Class
    {
        [Key]
        public String? Id { get; set; }
        // FK1
        public String? MajorId { get; set; }
        // FK2
        public String? HeadTeacherId { get; set; }
        // FK3
        public String? ClassPresidentId { get; set; }
        public String? Name { get; set; }
        public int AcademicYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<User>? Users { get; set; }
        public Major? Major { get; set; }
        public User? HeadTeacher { get; set; }
        public User? ClassPresident { get; set; }
    }
}
