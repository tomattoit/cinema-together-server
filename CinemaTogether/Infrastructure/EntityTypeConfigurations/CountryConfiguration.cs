using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(EntityLimitations.MaxCountryNameLength);
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}
