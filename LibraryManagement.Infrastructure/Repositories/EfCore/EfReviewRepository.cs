

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;

namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public EfReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public Review Add(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();

            return review;
        }

        public void Delete(int id)
        {
            var review = GetById(id);

            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public bool ExistsByUserAndBook(int userId, int bookId)
        {
            return _context.Reviews.Any(r => r.UserId == userId && r.BookId == bookId);
        }

        public IEnumerable<Review> GetByBookId(int bookId)
        {
            return [.. _context.Reviews.Where(r => r.BookId == bookId)];
        }

        public Review GetById(int id)
        {
            var review = _context.Reviews.Find(id);

            if(review is null)
                throw new NotFoundException($"Review with Id: {id} not found.");

            return review;
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }
    }
}
