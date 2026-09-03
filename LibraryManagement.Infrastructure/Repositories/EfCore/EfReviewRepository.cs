

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
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

        public IEnumerable<ApprovedReviewDto> GetApprovedReviewsByBookId(int bookId)
        {
            return [.. _context.Reviews
    .Where(x => x.BookId == bookId &&
                x.Status == ReviewStatusEnum.Approved)
    .Select(x => new ApprovedReviewDto
    {
        ReviewId = x.Id,
        Username = x.User.Username,
        Comment = x.Comment,
        Rating = x.Rating,
        CreatedAt = x.CreatedAt
    })];
        }


        public Review GetById(int id)
        {
            var review = _context.Reviews.Find(id);

            if (review is null)
                throw new NotFoundException($"Review with Id: {id} not found.");

            return review;
        }

        public IEnumerable<UserReviewDto> GetByUserId(int userId)
        {
            return [.._context.Reviews
               .Where(x => x.UserId == userId)
               .Select(x=> new UserReviewDto()
               {
                   BookTitle = x.Book.Title,
                   Comment = x.Comment,
                   Rating = x.Rating,
                   Status = x.Status,
                   ReviewId = x.Id,
                   CreatedAt = x.CreatedAt,
                   UpdatedAt = x.UpdatedAt
               })];
        }

        public IEnumerable<PendingReviewDto> GetPendingReviews()
        {
            return [.._context.Reviews
               .Where(x=>x.Status == ReviewStatusEnum.Pending)
               .Select(x=> new PendingReviewDto
               {
               ReviewId = x.Id,
               Username = x.User.Username,
               BookTitle = x.Book.Title,
               Comment = x.Comment,
               Rating = x.Rating,
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt
               })];
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        //================================
        public double? CalculateAverageRating(int bookId)
        {
            return _context.Reviews.
                 Where(x => x.BookId == bookId && x.Status == ReviewStatusEnum.Approved)
                 .Select(x => (double?)x.Rating)
                 .Average();
        }
    }
}
