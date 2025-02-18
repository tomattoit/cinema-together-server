using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(EntityLimitations.MaxEmailLength);
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(EntityLimitations.MaxPasswordHashLength);
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(EntityLimitations.MaxUsernameLength);
        builder.HasIndex(x => x.Username)
            .IsUnique();
        builder.Property(x => x.Name)
            .HasMaxLength(EntityLimitations.MaxNameLength);
        builder.Property(x => x.ProfilePicturePath)
            .HasMaxLength(EntityLimitations.MaxImagePathLength);
        builder.Property(x => x.Rating)
            .HasPrecision(EntityLimitations.RatingPrecision, EntityLimitations.RatingScale);
    }
}
