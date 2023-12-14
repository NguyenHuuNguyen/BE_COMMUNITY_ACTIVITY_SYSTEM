using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class CommunityActivity
    {
        [Key]
        public String? Id { get; set; }
        // FK1
        public String? UserId { get; set; }
        // FK2
        public String? ActivityTpeId { get; set; }
        public String? Name { get; set; }
        public int Year { get; set; }
        public int SelfEvaluationScore { get; set; }
        public int ClassPresidentEvaluationScore { get; set; }
        public String? EvidentLink { get; set; }
        public int Status { get; set; }
        public String? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public User? User { get; set; }
        public CommunityActivityType? CommunityActivityType { get; set; }
    }
}
