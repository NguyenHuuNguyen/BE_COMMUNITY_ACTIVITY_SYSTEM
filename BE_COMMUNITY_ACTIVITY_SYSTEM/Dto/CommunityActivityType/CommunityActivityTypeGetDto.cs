namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.CommunityActivityType
{
    public class CommunityActivityTypeGetDto
    {
        public String? Id { get; set; }
        public String? Name { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
    }
}
