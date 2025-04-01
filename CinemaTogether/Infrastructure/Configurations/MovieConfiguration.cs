using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasIndex(m => m.Id).IsUnique();
        
        builder.Property(m => m.Title).IsRequired();
        
        builder.Property(m => m.Rating).HasDefaultValue(0);
    }
}