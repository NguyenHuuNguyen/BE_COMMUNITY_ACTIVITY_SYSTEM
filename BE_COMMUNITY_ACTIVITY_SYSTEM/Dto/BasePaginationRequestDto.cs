using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto
{
    public class BasePaginationRequestDto
    {
        public int ItemPerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string? Filter { get; set; } = "";
        public void ValidateInput()
        {
            ItemPerPage = ItemPerPage <= 0 ? 10 : ItemPerPage;
            Page = Page <= 0 ? 1 : Page;
            Filter = Filter == null ? String.Empty : Filter.ToLower();
        }
    }
}
