using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class DeckConfiguration : IEntityTypeConfiguration<Deck>
    {
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(d => d.Workspace)
                   .WithMany(w => w.Decks)
                   .HasForeignKey(d => d.WorkspaceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
