

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;

        public BookService(IBookRepository bookRepo, ICategoryRepository categoryRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
        }
        public void ChangeCategory(int bookId, int categoryId)
        {
            var book = GetById(bookId);
            var category = _categoryRepo.GetById(categoryId);
            if (category is null)
                throw new NotFoundException("Category not found");
            book.ChangeCategory(category);

            _bookRepo.Update(book);
        }

        public void ChangeTitle(int bookId, string title)
        {
            var book = GetById(bookId);
            book.ChangeTitle(title);

            _bookRepo.Update(book);
        }

        public Book Create(string title, Category category)
        {
            var book = new Book(title, category);
            if(_bookRepo.ExistsByTitle(title))
                throw new DuplicateException("title is already exists");
            _bookRepo.Add(book);
            return book;
        }

        public void Delete(int bookId)
        {
            
            var book =  GetById(bookId);
            if(book.BookLoans.Any(l => !l.IsReturned))
                throw new BusinessRuleException("Cannot delete borrowed book");

            _bookRepo.Delete(bookId);
        }

        public List<Book> GetAll()
        {
            return _bookRepo.GetAll();
        }

        public Book GetById(int bookId)
        {
            var book = _bookRepo.GetById(bookId);
            return book is null ? throw new NotFoundException("Book not found") : book;
                
        }
        public List<Book> GetBooksByCategory(int categoryId)
        {
            return _bookRepo.GetByCategoryId(categoryId);
        }
    }
}
