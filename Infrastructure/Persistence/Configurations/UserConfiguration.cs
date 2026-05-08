using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.UserLanguages)
                   .WithOne(ul => ul.User)
                   .HasForeignKey(ul => ul.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
