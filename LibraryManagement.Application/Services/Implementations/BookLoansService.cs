

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Application.Services.Implementations
{
    public class BookLoansService : IBookLoansService
    {
        private readonly IBookLoanRepository _loanRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookRepository _bookRepo;

        public BookLoansService(IBookLoanRepository loanRepo, IUserRepository userRepo, IBookRepository bookRepo)
        {
            _loanRepo = loanRepo;
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }


        public BookLoan GetLoanById(int id)
        {
            var loan = _loanRepo.GetById(id);
            return loan is null ? throw new NotFoundException("Loan not found") : loan;
        }


        public BookLoan CreateLoanBook(int userId, int bookId)
        {
            var user = _userRepo.GetById(userId);
            if (user is null)
                throw new NotFoundException("User not found");

            var book = _bookRepo.GetById(bookId);
            if (book is null)
                throw new NotFoundException("Book not found");

            if (!_loanRepo.IsAvailable(bookId))
                throw new BusinessRuleException("Book is not available for loan");

            var borrowDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var loan = new BookLoan(user, book, borrowDate);

            _loanRepo.Add(loan);

            return loan;
        }

        public void ReturnBook(int userId, int bookLoanId)
        {

            var bookLoan = _loanRepo.GetById(bookLoanId) ?? throw new NotFoundException("Book loan not found");

            if (bookLoan.UserId != userId)
                throw new BusinessRuleException("This loan does not belong to this user.");

            if (bookLoan.IsReturned)
                throw new BusinessRuleException("Book has already been returned.");

            var returnDate = DateOnly.FromDateTime(DateTime.UtcNow);

            bookLoan.MarkAsReturned(returnDate);

            _loanRepo.Update(bookLoan);
        }

        public IEnumerable<UserLoanDto> GetUserLoans(int userId)
        {
            var User = _userRepo.GetById(userId);

            return [.. _loanRepo.GetUserLoans(userId)
                .Select(x => new UserLoanDto{
                    BookLoanId = x.Id,
                    BookTitle = x.Book.Title,
                    BorrowDate= x.BorrowDate,
                    ReturnDate = x.ReturnDate,
                    IsReturned = x.IsReturned,
                })];
        }

        public IEnumerable<BookLoanDto> GetActiveLoans()
        {
            return [.. _loanRepo.GetActiveLoans()
                .Select(x => new BookLoanDto{
            BookLoanId = x.Id,
            Username = x.User.Username,
            BookTitle = x.Book.Title,
            BorrowDate = x.BorrowDate
                })];
        }
    }
}
