using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity
{
    public class CommunityActivityCreateDto
    {
        [Required]
        public String? UserId { get; set; }
        [Required]
        public String? ActivityTypeId { get; set; }
        [Required]
        public String? Name { get; set; }
        public int Year { get; set; }
        public int SelfEvaluationScore { get; set; }
        public int ClassPresidentEvaluationScore { get; set; }
        public String? EvidentLink { get; set; }
        public int Status { get; set; }
        public String? StudentNote { get; set; }
        public String? AdminNote { get; set; }
    }
}
