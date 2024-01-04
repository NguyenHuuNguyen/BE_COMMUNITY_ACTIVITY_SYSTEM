using System.ComponentModel.DataAnnotations;

namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto.Class
{
    public class ClassPaginationRequestDto
    {
        public int ItemPerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int AcademyYear { get; set; } = 0;
        public string? MajorId { get; set; } = "";
        public void ValidateInput()
        {
            ItemPerPage = ItemPerPage <= 0 ? 10 : ItemPerPage;
            Page = Page <= 0 ? 1 : Page;
            MajorId ??= string.Empty;
        }
    }
}
