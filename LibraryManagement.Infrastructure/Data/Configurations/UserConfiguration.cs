using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace LibraryManagement.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("UserId");

            builder
                .HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.Username)
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .HasMaxLength(50);

        }
    }
}
