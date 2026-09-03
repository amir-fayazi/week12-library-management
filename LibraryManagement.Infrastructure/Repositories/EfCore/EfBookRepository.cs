using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


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
            return [.._context.Books.Include(x => x.Category)];
        }

        public List<Book> GetAllAvailable()
        {
            return [.._context.Books
                .Include(x=> x.Category)
                .Where(book => book.BookLoans
                .All(loan => loan.IsReturned)
                )];
        }

        public List<Book> GetAllBorrowed()
        {
            return _context.BookLoans.Where(p => !p.IsReturned).Select(p => p.Book).ToList();
        }

        public Book GetById(int id)
        {
            var book = _context.Books
                .Include(x => x.Category)
                .Include(x => x.BookLoans)
                .FirstOrDefault(x => x.Id == id);

            if (book is null)
                throw new NotFoundException($"Book with Id: {id} not found.");

            return book;
        }
        public List<Book> GetByCategoryId(int categoryId)
        {
            return [.._context.Books
             .Include(x => x.Category)
             .Where(x => x.CategoryId == categoryId)
             ];
        }

        public void Update(Book updatedBook)
        {
            _context.Books.Update(updatedBook);
            _context.SaveChanges();
        }
    }
}
