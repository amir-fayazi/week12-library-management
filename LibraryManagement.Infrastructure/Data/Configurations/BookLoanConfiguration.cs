using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class BookLoanConfiguration
        : IEntityTypeConfiguration<BookLoan>
    {
        public void Configure(
            EntityTypeBuilder<BookLoan> builder)
        {
            builder.ToTable("BookLoans");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("BookLoanId");

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookLoans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                .WithMany(x => x.BookLoans)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}