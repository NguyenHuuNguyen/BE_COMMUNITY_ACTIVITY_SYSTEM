﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Models
{
    public class CommunityActivityType
    {
        [Key]
        public String? Id { get; set; }
        public String? Name { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public String? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<CommunityActivity>? CommunityActivities { get; set; }
    }
}
