

using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Contracts.Services
{
    public interface IWishListService
    {
        Wishlist CreateWishList(int userId, int BookId);
        void DeleteWishList(int userId, int wishlistId);
        IEnumerable<WishlistDto> GetUserWishList(int userId);

    }
}
