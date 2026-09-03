

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IBookLoansService
    {
        BookLoan CreateLoanBook(int userId, int bookId);

        void ReturnBook(int userId, int bookLoanId);

        IEnumerable<UserLoanDto> GetUserLoans(int userId);

        IEnumerable<BookLoanDto> GetActiveLoans();

        BookLoan GetLoanById(int id);
    }
}
