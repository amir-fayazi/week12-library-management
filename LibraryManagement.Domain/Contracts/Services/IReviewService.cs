

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IReviewService
    {
        void CreateReview(int userId, int bookId, string? comment, int rating);
        void DeleteReview(int userId, int reviewId);
        void ChangeRating(int userId, int reviewId, int rating);
        void ChangeComment(int userId, int reviewId, string comment);

        //=========================================
        IEnumerable<UserReviewDto> GetAllUserReviews(int userId);
        void ApproveReview(int reviewId);
        void RejectReview(int reviewId);
    }
}
