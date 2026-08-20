using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;


namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfBookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public EfBookRepository(AppDbContext context)
        {
            _context = context;
        }
        public Book Add(Book newBook)
        {
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook;
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if(book is null)
                throw new NotFoundException("Book not found for deletion.");
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public bool ExistsByTitle(string title)
        {
            return _context.Books.Any(p => p.Title == title);
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetAllAvailable()
        {
            return _context.Books.Where(book => book.BookLoans.All(loan => loan.IsReturned)).ToList();
        }

        public List<Book> GetAllBorrowed()
        {
            return _context.BookLoans.Where(p => !p.IsReturned).Select(p => p.Book).ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books.Find(id);
        }
        public List<Book> GetByCategoryId(int categoryId)
        {
            return _context.Books.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Book updatedBook)
        {
            _context.Books.Update(updatedBook);
            _context.SaveChanges();
        }
    }
}
