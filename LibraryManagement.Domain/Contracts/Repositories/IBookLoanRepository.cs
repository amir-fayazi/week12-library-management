using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Repositories
{
    public interface IBookLoanRepository
    {
        BookLoan Add(BookLoan bookLoan);

        BookLoan? GetById(int id);
        List<Book> GetBorrowedBooksByUserId(int userId);
        User? GetBorrowerByBookId(int bookId);

        bool IsAvailable(int bookId);

        void Update(BookLoan updatedBookLoan);
        void Delete(int id);

        public List<BookLoan> GetActiveLoans();
        List<BookLoan> GetUserLoans(int userId);
    }
}
