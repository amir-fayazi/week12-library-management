

using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Application.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookLoanRepository _loanRepo;
        public ReviewService(IUserRepository userRepo, IBookRepository bookRepo, IReviewRepository reviewRepo, IBookLoanRepository loanRepo)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
            _loanRepo = loanRepo;
        }

        public void ApproveReview(int reviewId)
        {
            var review = _reviewRepo.GetById(reviewId);
            review.Approve();
            _reviewRepo.Update(review);

        }

        public void RejectReview(int reviewId)
        {
            var review = _reviewRepo.GetById(reviewId);
            review.Reject();
            _reviewRepo.Update(review);

        }
        public void ChangeComment(int userId, int reviewId, string comment)
        {
            var review = _reviewRepo.GetById(reviewId);
            if (review.UserId != userId)
                throw new BusinessRuleException("only owner can edit ");
            review.Pending();
            _reviewRepo.Update(review);
        }

        public void ChangeRating(int userId, int reviewId, int rating)
        {
            var review = _reviewRepo.GetById(reviewId);
            if (review.UserId != userId)
                throw new BusinessRuleException("only owner can edit ");
            review.ChangeRating(rating);
            review.Pending();
            _reviewRepo.Update(review);

        }

        public void CreateReview(int userId, int bookId, string? comment, int rating)
        {
            var user = _userRepo.GetById(userId);
            var book = _bookRepo.GetById(bookId);
            if (!_loanRepo.HasUserBorrowedBook(userId, bookId))
                throw new BusinessRuleException("");

            if (_reviewRepo.ExistsByUserAndBook(userId, bookId))
                throw new DuplicateException("duplicate review");

            var review = new Review(userId, bookId, comment, rating);

            _reviewRepo.Add(review);
        }

        public void DeleteReview(int userId, int reviewId)
        {
            var review = _reviewRepo.GetById(reviewId);
            if (review.UserId != userId)
                throw new BusinessRuleException("it is not blong");

            _reviewRepo.Delete(reviewId);

        }

        public IEnumerable<UserReviewDto> GetAllUserReviews(int userId)
        {
            return [.. _reviewRepo.GetByUserId(userId)];
        }

    }
}
