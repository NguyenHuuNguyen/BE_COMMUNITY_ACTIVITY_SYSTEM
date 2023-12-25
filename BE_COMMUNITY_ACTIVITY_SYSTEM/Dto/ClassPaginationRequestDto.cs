﻿namespace BE_COMMUNITY_ACTIVITY_SYSTEM.Dto
{
    public class ClassPaginationRequestDto
    {
        public int ItemPerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int? AcademyYear { get; set; }
        public string? MajorName { get; set; } = "";
        public void ValidateInput()
        {
            ItemPerPage = ItemPerPage <= 0 ? 10 : ItemPerPage;
            Page = Page <= 0 ? 1 : Page;
            AcademyYear = AcademyYear == null ? null : AcademyYear;
            MajorName = MajorName == null ? String.Empty : MajorName.ToLower();
        }
    }
}
