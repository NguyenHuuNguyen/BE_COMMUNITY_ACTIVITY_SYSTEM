﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class CommunityActivity
    {
        [Key]
        public String? Id { get; set; }
        // FK1
        public String? UserId { get; set; }
        // FK2
        public String? ActivityTypeId { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public User? User { get; set; }
        public CommunityActivityType? CommunityActivityType { get; set; }
    }
}
