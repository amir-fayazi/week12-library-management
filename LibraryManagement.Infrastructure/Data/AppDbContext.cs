

using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 @"Server=.;Database=Week12-LibraryManagementDb;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .HasColumnName("UserId");

            modelBuilder.Entity<Book>()
                .Property(x => x.Id)
                .HasColumnName("BookId");

            modelBuilder.Entity<Category>()
                .Property(x => x.Id)
                .HasColumnName("CategoryId");

            modelBuilder.Entity<BookLoan>()
                .Property(x => x.Id)
                .HasColumnName("BookLoanId");
        }
    }
}
