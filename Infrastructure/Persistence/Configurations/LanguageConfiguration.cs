using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Code).IsRequired().HasMaxLength(10);
            builder.Property(l => l.FlagEmoji).HasMaxLength(10);

            builder.HasIndex(l => l.Code).IsUnique();

            // Seed data for languages
            builder.HasData(
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Português", Code = "pt", FlagEmoji = "🇧🇷", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Inglês", Code = "en", FlagEmoji = "🇺🇸", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Japonês", Code = "ja", FlagEmoji = "🇯🇵", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Chinês", Code = "zh", FlagEmoji = "🇨🇳", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Espanhol", Code = "es", FlagEmoji = "🇪🇸", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Francês", Code = "fr", FlagEmoji = "🇫🇷", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Alemão", Code = "de", FlagEmoji = "🇩🇪", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Coreano", Code = "ko", FlagEmoji = "🇰🇷", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Italiano", Code = "it", FlagEmoji = "🇮🇹", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Russo", Code = "ru", FlagEmoji = "🇷🇺", CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
