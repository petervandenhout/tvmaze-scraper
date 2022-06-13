using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TvMazeScraper.Entities;

namespace TvMazeScraper.EntityFrameworkCore.Configuration
{
    internal class TvShowEntityTypeConfiguration : IEntityTypeConfiguration<TvShow>
    {
        public void Configure(EntityTypeBuilder<TvShow> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
            builder.HasMany(e => e.Cast).WithMany(e => e.TvShows);
        }
    }
}
