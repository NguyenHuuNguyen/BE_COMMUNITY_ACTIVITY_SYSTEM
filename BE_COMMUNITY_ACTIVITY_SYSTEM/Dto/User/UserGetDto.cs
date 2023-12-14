namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.User
{
    public class UserGetDto
    {
        public string? ClassId { get; set; }
        public string? Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public int Gender { get; set; }
        public string? Ethnic { get; set; }
        public string? Nationality { get; set; }
        public string? IdentificationCardId { get; set; }
        public DateTime IdentificationCardIssueDate { get; set; }
        public string? IdentificationCardIssuePlace { get; set; }
        public string? Religion { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? StudentId { get; set; }
        public string? TeacherId { get; set; }
        public int Status { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
    }
}
