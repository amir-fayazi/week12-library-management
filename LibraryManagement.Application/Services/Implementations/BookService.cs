

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IReviewRepository _reviewRepo;

        public BookService(IBookRepository bookRepo, ICategoryRepository categoryRepo, IReviewRepository reviewRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
            _reviewRepo = reviewRepo;
        }
        public void ChangeCategory(int bookId, int categoryId)
        {
            var book = GetBookById(bookId);
            if (book is null)
                throw new NotFoundException("Book not found");

            var category = _categoryRepo.GetById(categoryId);
            if (category is null)
                throw new NotFoundException("Category not found");

            book.ChangeCategory(category);

            _bookRepo.Update(book);
        }

        public void ChangeTitle(int bookId, string title)
        {
            var book = GetBookById(bookId);
            book.ChangeTitle(title);

            _bookRepo.Update(book);
        }

        public Book CreateBook(string title, int categoryId)
        {
            var category = _categoryRepo.GetById(categoryId);
            if (category is null)
                throw new NotFoundException("Category not found");

            var book = new Book(title, category);

            if(_bookRepo.ExistsByTitle(title))
                throw new DuplicateException("title is already exists");
            _bookRepo.Add(book);
            return book;
        }

        public void DeleteBook(int bookId)
        {
            
            var book = GetBookById(bookId);
            if(book.BookLoans.Any())
                throw new BusinessRuleException("Cannot delete book because loan history exists.");

            _bookRepo.Delete(bookId);
        }

        
        public Book GetBookById(int bookId)
        {
            var book = _bookRepo.GetById(bookId);
            return book is null ? throw new NotFoundException("Book not found") : book;
                
        }


        public BookResultDto GetBookDetails(int bookId)
        {
            var book = _bookRepo.GetById(bookId);
            return new BookResultDto
            {
                BookId = book.Id,
                CategoryName = book.Category.Name,
                Title = book.Title,
                AverageRating = _reviewRepo.CalculateAverageRating(bookId),
                Reviews = _reviewRepo.GetApprovedReviewsByBookId(bookId)
            };
                
        }

        public IEnumerable<BookListDto> GetAllBooks()
        {
            return [.. _bookRepo.GetAll()
                .Select(x=> new BookListDto
                {
                    BookId = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title
                })];
        }

        public IEnumerable<BookListDto> GetAllAvailableBooks()
        {
            return [.. _bookRepo.GetAllAvailable()
                .Select(x=> new BookListDto
                {
                    BookId = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title
                })];
        }

        public IEnumerable<BookListDto> GetBooksByCategory(int categoryId)
        {
            return [.. _bookRepo.GetByCategoryId(categoryId)
                .Select(x=> new BookListDto
                {
                    BookId = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title
                })];
        }
    }
}
