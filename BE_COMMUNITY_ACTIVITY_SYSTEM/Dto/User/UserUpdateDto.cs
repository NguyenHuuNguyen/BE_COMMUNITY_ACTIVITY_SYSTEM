﻿using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserUpdateDto
    {
        public string? ClassId { get; set; }
        [Required]
        public string? Id { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        [Required]
        public int Gender { get; set; }
        public string? Ethnic { get; set; }
        public string? Nationality { get; set; }
        [Required]
        public string? IdentificationCardId { get; set; }
        public DateTime IdentificationCardIssueDate { get; set; }
        public string? IdentificationCardIssuePlace { get; set; }
        public string? Religion { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public int Status { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
    }
}
