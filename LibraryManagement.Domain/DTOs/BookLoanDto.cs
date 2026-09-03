namespace LibraryManagement.Domain.DTOs
{
    public class BookLoanDto
    {
        public int BookLoanId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string BookTitle { get; set; } = string.Empty;

        public DateOnly BorrowDate { get; set; }
    }
}