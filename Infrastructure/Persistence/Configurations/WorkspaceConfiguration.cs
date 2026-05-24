using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).IsRequired().HasMaxLength(100);

            builder.HasIndex(w => new { w.UserId, w.Name }).IsUnique();

            builder.HasOne(w => w.User)
                   .WithMany(u => u.Workspaces)
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(w => w.Language)
                   .WithMany()
                   .HasForeignKey(w => w.LanguageId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.NativeLanguage)
                   .WithMany()
                   .HasForeignKey(w => w.NativeLanguageId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
