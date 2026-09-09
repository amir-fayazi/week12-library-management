

using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
             .ToTable("Books");

            builder.HasKey(x => x.Id);
    

            builder
             .Property(x => x.Id)
             .HasColumnName("BookId");

            builder
             .HasIndex(x => x.Title)
             .IsUnique();

            builder
             .HasOne(book => book.Category)
             .WithMany(category => category.Books)
             .HasForeignKey(book => book.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            builder
             .Property(x => x.Title)
             .HasMaxLength(150);
        }
    }
}
