﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class
{
    public class ClassCreateDto
    {
        [Required]
        public String? MajorId { get; set; }
        [Required]
        public String? HeadTeacherId { get; set; }
        [Required]
        public String? Name { get; set; }
        [Required]
        public int AcademicYear { get; set; }
    }
}
