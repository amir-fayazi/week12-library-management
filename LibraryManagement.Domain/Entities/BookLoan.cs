
using LibraryManagement.Domain.Exceptions;


namespace LibraryManagement.Domain.Entities
{
    public class BookLoan : BaseEntity
    {
        public Book Book { get; private set; } 
        public User User { get; private set; }
        public DateOnly BorrowDate { get; private set; }
        public bool IsReturned { get; private set; } = false;

        public BookLoan()
        {
            
        }
        public BookLoan(User user, Book book, DateOnly borrowDate)
        {
            ValidateUser(user);
            ValidateBook(book);
            ValidateBorrowDate(borrowDate);

            User = user;
            Book = book;
            BorrowDate = borrowDate;
        }



        private void ValidateUser(User user)
        {
            if (user is null)
                throw new ValidationException("Borrowed book must have a valid user.");
        }
        private void ValidateBook(Book book)
        {
            if (book is null)
                throw new ValidationException("Borrowed book must have a valid book.");
        }
        private void ValidateBorrowDate(DateOnly borrowDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (borrowDate > today)
                throw new ValidationException("Borrow date cannot be in the future.");
        }

        public void MarkAsReturned()
        {
            IsReturned = true;
        }

    }

}
