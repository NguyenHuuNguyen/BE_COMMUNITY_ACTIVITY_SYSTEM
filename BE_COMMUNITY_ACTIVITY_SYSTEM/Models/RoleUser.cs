using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class RoleUser
    {
        [Key]
        public String? Id { get; set; }
        // FK1
        public String? RoleId { get; set; }
        // FK2
        public String? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public Role? Role { get; set; }
        public User? User { get; set; }
    }
}
