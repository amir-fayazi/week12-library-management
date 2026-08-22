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
            //-------------------------Configure column names

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


            //-------------------------Configure unique constraints

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasIndex(x => x.Title)
                .IsUnique();


            //-------------------------Configure relationships

            modelBuilder.Entity<Book>()
                .HasOne(book => book.Category)
                .WithMany(category => category.Books)
                .HasForeignKey(book => book.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(loan => loan.Book)
                .WithMany(book => book.BookLoans)
                .HasForeignKey(loan => loan.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(loan => loan.User)
                .WithMany(user => user.BookLoans)
                .HasForeignKey(loan => loan.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}