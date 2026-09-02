using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.DTOs
{
    public class UserReviewDto
    {
        public int ReviewId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public string? Comment { get; set; }

        public int Rating { get; set; }

        public ReviewStatusEnum Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}