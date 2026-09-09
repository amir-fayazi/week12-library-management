using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Contracts.Repositories
{
    public interface IWishlistRepository
    {
        Wishlist Add(Wishlist wishlist);

        void Delete(int id);

        IEnumerable<Wishlist> GetByUserId(int userId);

        bool ExistsByUserAndBook(int userId, int bookId);

        int CountByBookId(int bookId);
        Wishlist GetById(int id);
    }
}
