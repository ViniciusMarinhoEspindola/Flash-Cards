using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class UserLanguageConfiguration : IEntityTypeConfiguration<UserLanguage>
    {
        public void Configure(EntityTypeBuilder<UserLanguage> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(ul => ul.User)
                   .WithMany(ul => ul.UserLanguages)
                   .HasForeignKey(ul => ul.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ul => ul.Language)
                   .WithMany(ul => ul.UserLanguages)
                   .HasForeignKey(ul => ul.LanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ul => ul.NativeLanguage)
                   .WithMany()
                   .HasForeignKey(ul => ul.NativeLanguageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ul => new { ul.UserId, ul.LanguageId }).IsUnique();
        }
    }
}
