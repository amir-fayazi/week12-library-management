

namespace LibraryManagement.Domain.DTOs
{
    public class WishlistDto
    {
        public int WishlistId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }

}
