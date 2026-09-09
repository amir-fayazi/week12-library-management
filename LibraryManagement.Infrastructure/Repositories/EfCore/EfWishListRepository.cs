using LibraryManagement.Domain.Contracts.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagement.Infrastructure.Repositories.EfCore
{
    public class EfWishListRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;
        public EfWishListRepository(AppDbContext context)
        {
            _context = context;
        }
        public Wishlist GetById(int id)
        {
            var wishlist = _context.Wishlists
                .Include(x=>x.Book)
                .Include(x=>x.User)
                .FirstOrDefault(x => x.Id == id);
            if (wishlist is null)
                throw new NotFoundException($"Wishlist with Id: {id} not found.");

            return wishlist;
        }
        public Wishlist Add(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();
            return wishlist;
        }

        public int CountByBookId(int bookId)
        {
            return  _context.Wishlists.Count(x => x.BookId == bookId);

        }

        public void Delete(int id)
        {
            var wishlist = GetById(id);
            _context.Wishlists.Remove(wishlist);
            _context.SaveChanges();
        }

        public bool ExistsByUserAndBook(int userId, int bookId)
        {
            return _context.Wishlists.Any(x => x.UserId == userId && x.BookId == bookId);
        }

        public IEnumerable<Wishlist> GetByUserId(int userId) // Do I need Dto???
        {
            return [.._context.Wishlists
                .Include(x => x.Book)
                    .ThenInclude(x => x.Category)
                .Where(x => x.UserId == userId)
                ];
        }
    }
}
