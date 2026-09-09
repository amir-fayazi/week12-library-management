using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Contracts.Services;
using LibraryManagement.Domain.DTOs;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Application.Services.Implementations
{
    public class WishlistService : IWishListService
    {
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookRepository _bookRepo;

        public WishlistService(
            IWishlistRepository wishlistRepo,
            IUserRepository userRepo,
            IBookRepository bookRepo)
        {
            _wishlistRepo = wishlistRepo;
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }

        public Wishlist CreateWishlist(int userId, int bookId)
        {
            _userRepo.GetById(userId);
            _bookRepo.GetById(bookId);

            if (_wishlistRepo.ExistsByUserAndBook(userId, bookId))
                throw new BusinessRuleException(
                    "This book is already in your wishlist.");

            var wishlist = new Wishlist(userId, bookId);

            return _wishlistRepo.Add(wishlist);
        }

        public void DeleteWishlist(int userId, int wishlistId)
        {
            _userRepo.GetById(userId);

            var wishlist = _wishlistRepo.GetById(wishlistId);

            if (wishlist.UserId != userId)
                throw new BusinessRuleException(
                    "You can only delete items from your own wishlist.");

            _wishlistRepo.Delete(wishlistId);
        }

        public IEnumerable<WishlistDto> GetUserWishlist(int userId)
        {
            _userRepo.GetById(userId);

            var items = _wishlistRepo.GetByUserId(userId);

            return items.Select(x => new WishlistDto
            {
                WishlistId = x.Id,
                BookTitle = x.Book.Title,
                CategoryName = x.Book.Category.Name,
                CreatedAt = x.CreatedAt
            });
        }
    }
}