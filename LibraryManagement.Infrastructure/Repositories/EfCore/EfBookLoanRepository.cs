
using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;

namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfBookLoanRepository : IBookLoanRepository
    {
        private readonly AppDbContext _context;

        public EfBookLoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public BookLoan Add(BookLoan bookLoan)
        {
            _context.BookLoans.Add(bookLoan);
            _context.SaveChanges();

            return bookLoan;
        }

        public void Delete(int id)
        {
            var bookLoan = GetById(id);
            if (bookLoan is null)
                throw new NotFoundException("bookLoan not found for deletion.");
            _context.BookLoans.Remove(bookLoan);
            _context.SaveChanges();
        }

        public List<Book> GetBorrowedBooksByUserId(int userId)
        {
            return _context.BookLoans
                .Where(p => p.User.Id == userId && !p.IsReturned)
                .Select(p=> p.Book)
                .ToList();
                       
        }

        public User? GetBorrowerByBookId(int bookId)
        {
           return _context.BookLoans
                .Where(p => p.Book.Id == bookId && !p.IsReturned)
                .Select(p => p.User)
                .FirstOrDefault();
        }

        public BookLoan GetById(int id)
        {
            var loan = _context.BookLoans.Find(id);

            if (loan is null)
                throw new NotFoundException($"Loan with Id: {id} not found.");

            return loan;
        }

        public bool IsAvailable(int bookId)
        {
            return !_context.BookLoans.Any(p => p.Book.Id == bookId && !p.IsReturned);
        }   

        public void Update(BookLoan updatedBookLoan)
        {
            _context.BookLoans.Update(updatedBookLoan);
            _context.SaveChanges();
        }

        public List<BookLoan> GetActiveLoans()
        {
            return _context.BookLoans.Where(x => !x.IsReturned).ToList();
        }
        public List<BookLoan> GetUserLoans(int userId)
        {
            return _context.BookLoans
                .Where(x => x.User.Id == userId)
                .ToList();
        }

        public bool HasUserBorrowedBook(int userId, int bookId)
        {
            return _context.BookLoans
                 .Any(x => x.UserId == userId && x.BookId == bookId);
        }
    }
}

