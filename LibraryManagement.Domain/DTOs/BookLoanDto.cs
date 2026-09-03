namespace LibraryManagement.Domain.DTOs
{
    public class BookLoanDto
    {
        public int BookLoanId { get; set; }

        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public DateOnly BorrowDate { get; set; }

        public DateOnly? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
    }
}