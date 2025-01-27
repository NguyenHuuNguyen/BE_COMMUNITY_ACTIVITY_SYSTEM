﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserStatusUpdateDto
    {
        [Required]
        public string? UserId { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
