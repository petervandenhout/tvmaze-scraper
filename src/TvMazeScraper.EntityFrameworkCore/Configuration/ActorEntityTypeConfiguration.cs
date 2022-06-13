using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMazeScraper.Entities;

namespace TvMazeScraper.EntityFrameworkCore.Configuration
{
    internal class ActorEntityTypeConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Birthday).IsRequired();
        }
    }
}
