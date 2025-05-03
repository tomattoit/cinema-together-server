using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Description)
                .HasMaxLength(500);

            builder.Property(g => g.Type)
                .IsRequired()
                .HasMaxLength(20);

            // Отношения
            builder.HasOne(g => g.Owner)
                .WithMany()
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Chat)
                .WithOne(c => c.Group)
                .HasForeignKey<Group>(g => g.ChatId);

            builder.HasMany(g => g.Members)
                .WithMany()
                .UsingEntity(j => j.ToTable("GroupMembers"));

            builder.HasMany(g => g.PreferredGenres)
                .WithMany()
                .UsingEntity(j => j.ToTable("GroupPreferredGenres"));
        }
    }
}