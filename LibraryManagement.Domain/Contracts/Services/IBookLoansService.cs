

using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IBookLoansService
    {
        BookLoan CreateLoanBook(int userId, int bookId);

        void ReturnBook(int bookLoanId);

        List<BookLoan> GetUserLoans(int userId);

        List<BookLoan> GetActiveLoans();

        BookLoan GetLoanById(int id);
    }
}
