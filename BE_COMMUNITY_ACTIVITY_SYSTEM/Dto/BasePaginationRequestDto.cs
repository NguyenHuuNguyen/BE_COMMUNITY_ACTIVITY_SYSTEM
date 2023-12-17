namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto
{
    public class BasePaginationRequestDto
    {
        public int ItemPerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string? Filter { get; set; } = "";
    }
}
