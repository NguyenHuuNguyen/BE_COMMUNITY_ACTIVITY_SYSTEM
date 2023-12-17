namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Announcement
{
    public class AnnouncementGetDto
    {
        public String? Id { get; set; }
        public String? Title { get; set; }
        public String? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
