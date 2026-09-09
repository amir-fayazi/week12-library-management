

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IWishListService
    {
        Wishlist CreateWishlist(int userId, int bookId);
        void DeleteWishlist(int userId, int wishlistId);
        IEnumerable<WishlistDto> GetUserWishlist(int userId);

    }
}
