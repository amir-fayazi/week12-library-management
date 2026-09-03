

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Repositories
{
    public interface IReviewRepository
    {
        Review Add(Review review);
        void Delete(int id);
        void Update(Review updateReview);
        Review GetById(int id);
        bool ExistsByUserAndBook(int userId, int bookId);

        //--------------------
        
        IEnumerable<ApprovedReviewDto> GetApprovedReviewsByBookId(int bookId); 
        IEnumerable<PendingReviewDto> GetPendingReviews(); 
        IEnumerable<UserReviewDto> GetByUserId(int userId);

        //===================================
        double? CalculateAverageRating(int bookId);
    }
}
