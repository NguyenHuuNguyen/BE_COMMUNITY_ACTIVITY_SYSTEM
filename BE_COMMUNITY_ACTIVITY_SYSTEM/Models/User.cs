using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class User
    {
        // FK1
        public String? ClassId { get; set; }
        [Key]
        public String? Id { get; set; }
        public String? LastName { get; set; }
        public String? FirstName { get; set;}
        public DateTime DateOfBirth { get; set; }
        public String? PlaceOfBirth { get; set; }
        public int Gender { get; set; }
        public String? Ethnic { get; set; }
        public String? Nationality { get; set; }
        public String? IdentificationCardId { get; set; }
        public DateTime IdentificationCardIssueDate { get; set; }
        public String? IdentificationCardIssuePlace { get; set; }
        public String? Religion { get; set; }
        public String? Phone { get; set; }
        public String? Email { get; set; }
        public String? Facebook { get; set; }
        public String? StudentId { get; set; }
        public String? TeacherId { get; set; }
        public int Status { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public String? City { get; set; }
        public String? District { get; set; }
        public String? Ward { get; set; }
        public String? Street { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<RoleUser>? RoleUsers { get; set; }
        public ICollection<CommunityActivity>? CommunityActivities { get; set; }
        public Class? Class { get; set; }
        //public ICollection<Class>? Classes { get; set; } // If is TeacherHead
        //public Major? Major { get; set; } // If is MajorHead
    }
}
