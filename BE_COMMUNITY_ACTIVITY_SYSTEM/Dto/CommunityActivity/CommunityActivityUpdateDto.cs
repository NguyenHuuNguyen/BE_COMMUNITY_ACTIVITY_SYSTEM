using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivity
{
    public class CommunityActivityUpdateDto
    {
        [Required]
        public String? Id { get; set; }
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
        public String? ClassPresidentNote { get; set; }
        public String? HeadTeacherNote { get; set; }
        public String? MajorHeadNote { get; set; }
        public String? AdminNote { get; set; }
    }
}
