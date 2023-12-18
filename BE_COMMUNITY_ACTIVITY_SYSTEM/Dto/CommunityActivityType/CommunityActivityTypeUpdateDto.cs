﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType
{
    public class CommunityActivityTypeUpdateDto
    {
        [Required]
        public String? Id { get; set; }
        [Required]
        public String? Name { get; set; }
        [Required]
        public int MinScore { get; set; }
        [Required]
        public int MaxScore { get; set; }
    }
}
