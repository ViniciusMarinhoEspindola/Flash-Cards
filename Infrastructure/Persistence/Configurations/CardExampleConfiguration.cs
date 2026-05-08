using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CardExampleConfiguration : IEntityTypeConfiguration<CardExample>
    {
        public void Configure(EntityTypeBuilder<CardExample> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Sentence).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Translation).IsRequired().HasMaxLength(256);

            builder.HasOne(c => c.Card)
                   .WithMany(c => c.CardExamples)
                   .HasForeignKey(c => c.CardId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
