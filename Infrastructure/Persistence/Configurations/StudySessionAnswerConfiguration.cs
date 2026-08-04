using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class StudySessionAnswerConfiguration : IEntityTypeConfiguration<StudySessionAnswer>
    {
        public void Configure(EntityTypeBuilder<StudySessionAnswer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Rating).IsRequired();
            builder.Property(a => a.IsCorrect).IsRequired();
            builder.Property(a => a.LevelBefore).IsRequired();
            builder.Property(a => a.LevelAfter).IsRequired();
            builder.Property(a => a.AnsweredAt).IsRequired();

            builder.HasOne(a => a.StudySession)
                   .WithMany(s => s.Answers)
                   .HasForeignKey(a => a.StudySessionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Card)
                   .WithMany()
                   .HasForeignKey(a => a.CardId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                   .WithMany()
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.StudySessionId);
            builder.HasIndex(a => a.UserId);
        }
    }
}
