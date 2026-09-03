

namespace LibraryManagement.Domain.DTOs
{
    public class BookResultDto
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public double? AverageRating { get; set; }

        public IEnumerable<ApprovedReviewDto> Reviews { get; set; } = [];
    }
}
