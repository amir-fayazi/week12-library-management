

namespace LibraryManagement.Domain.DTOs
{
    public class PendingReviewDto
    {
        public int ReviewId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string BookTitle { get; set; } = string.Empty;

        public string? Comment { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
