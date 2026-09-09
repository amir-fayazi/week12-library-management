

namespace LibraryManagement.Domain.DTOs
{
    public class BookListDto
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;
        public double? AverageRating { get; set; }

        public string CategoryName { get; set; } = string.Empty;
        public int UsersWishlistCount { get; set; }
    }
}
