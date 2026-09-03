
namespace LibraryManagement.Domain.DTOs
{
    public class UserLoanDto
    {
        public int BookLoanId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public DateOnly BorrowDate { get; set; }

        public DateOnly? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
    }
}
