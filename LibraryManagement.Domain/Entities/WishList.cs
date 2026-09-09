using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entities
{
    public class Wishlist : BaseEntity
    {
        private Wishlist()
        {
        }

        public Wishlist(int userId, int bookId)
        {
            ValidateUserId(userId);
            ValidateBookId(bookId);

            UserId = userId;
            BookId = bookId;
            CreatedAt = DateTime.UtcNow;
        }

        public int UserId { get; private set; }

        public int BookId { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User User { get; private set; } = null!;

        public Book Book { get; private set; } = null!;


        private void ValidateUserId(int userId)
        {
            if (userId <= 0)
                throw new ValidationException(
                    "UserId must be greater than zero.");
        }

        private void ValidateBookId(int bookId)
        {
            if (bookId <= 0)
                throw new ValidationException(
                    "BookId must be greater than zero.");
        }
    }
}