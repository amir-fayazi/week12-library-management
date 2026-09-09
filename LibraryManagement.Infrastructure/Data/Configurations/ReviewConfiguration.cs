using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration
        : IEntityTypeConfiguration<Review>
    {
        public void Configure(
            EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews", table =>
            {
                table.HasCheckConstraint(
                    "CK_Reviews_Rating",
                    "[Rating] BETWEEN 1 AND 5");

                table.HasCheckConstraint(
                    "CK_Reviews_Status",
                    "[Status] IN (1, 2, 3)");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ReviewId");

            builder.HasIndex(x => new
            {
                x.UserId,
                x.BookId
            })
            .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Comment)
                .HasMaxLength(1000);
        }
    }
}