using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Term).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Translation).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Romanization).HasMaxLength(256);
            builder.Property(c => c.IsPhrase).IsRequired();
            builder.Property(c => c.Source).IsRequired();

            builder.HasOne(c => c.UserLanguage)
                   .WithMany(ul => ul.Cards)
                   .HasForeignKey(c => c.UserLanguageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
