using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CardProgressConfiguration : IEntityTypeConfiguration<CardProgress>
    {
        public void Configure(EntityTypeBuilder<CardProgress> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Level).IsRequired();
            builder.Property(c => c.Easiness).IsRequired();
            builder.Property(c => c.Interval).IsRequired();
            builder.Property(c => c.Repetitions).IsRequired();
            builder.Property(c => c.NextReview).IsRequired();

            builder.HasOne(c => c.Card)
                   .WithOne(c => c.Progress)
                   .HasForeignKey<CardProgress>(c => c.CardId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.User)
                  .WithMany()
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cp => cp.UserId);
            builder.HasIndex(cp => new { cp.UserId, cp.NextReview });
        }
    }
}
