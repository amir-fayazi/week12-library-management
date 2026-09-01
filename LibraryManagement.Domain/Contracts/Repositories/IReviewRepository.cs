

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
        IEnumerable<Review> GetByBookId(int bookId);
    }
}
