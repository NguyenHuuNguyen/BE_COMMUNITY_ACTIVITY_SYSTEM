namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto
{
    public class BasePaginationDto<T>
    {
        public ICollection<T>? Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public bool IsNextPage { get; set; }
        public bool IsPreviousPage { get; set; }
    }
}
