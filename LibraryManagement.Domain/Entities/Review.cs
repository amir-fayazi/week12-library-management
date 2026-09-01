using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Extensions;

namespace LibraryManagement.Domain.Entities
{
    public class Review : BaseEntity
    {
        private Review()
        {
        }

        public Review(
            int userId,
            int bookId,
            string? comment,
            int rating)
        {
            ValidateUserId(userId);
            ValidateBookId(bookId);
            ValidateRating(rating);
            ValidateComment(comment);

            UserId = userId;
            BookId = bookId;
            Comment = comment;
            Rating = rating;
            CreatedAt = DateTime.UtcNow;
        }


        public int UserId { get; private set; }

        public int BookId { get; private set; }

        public string? Comment { get; private set; }

        public int Rating { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public User User { get; private set; } = null!;

        public Book Book { get; private set; } = null!;


        public void EditComment(string editedText)
        {
            if (!editedText.IsValidText())
                throw new ValidationException("Comment cannot be empty.");

            ValidateComment(editedText);

            Comment = editedText;
            UpdatedAt = DateTime.UtcNow;
        }


        public void ChangeRating(int newRating)
        {
            ValidateRating(newRating);

            if (Rating == newRating)
                throw new ValidationException(
                    "New rating cannot be equal to current rating.");

            Rating = newRating;
        }


        private void ValidateRating(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ValidationException(
                    "Rating must be between 1 and 5.");
        }


        private void ValidateComment(string? comment)
        {
            if (comment != null && comment.Length > 1000)
                throw new ValidationException(
                    "Comment length cannot be more than 1000 characters.");
        }


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