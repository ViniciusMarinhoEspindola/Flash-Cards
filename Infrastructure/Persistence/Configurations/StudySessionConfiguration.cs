using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class StudySessionConfiguration : IEntityTypeConfiguration<StudySession>
    {
        public void Configure(EntityTypeBuilder<StudySession> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StartedAt).IsRequired();
            builder.Property(s => s.CardsReviewed).IsRequired();
            builder.Property(s => s.CorrectCount).IsRequired();
            builder.Property(s => s.IncorrectCount).IsRequired();

            builder.HasOne(s => s.User)
                   .WithMany()
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.UserId);
            builder.HasIndex(s => new { s.UserId, s.StartedAt });
        }
    }
}
