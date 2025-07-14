using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechFoods.Kitchen.Infrastructure.Repository.Configurations;
public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("MenuItems");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
               .HasColumnType("UNIQUEIDENTIFIER")
               .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(m => m.Name)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(m => m.Description)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(m => m.Price)
               .HasColumnType("DECIMAL(10,2)")
               .IsRequired();

        builder.Property(m => m.Category)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(m => m.IsAvailable)
               .HasColumnType("BIT")
               .IsRequired();

        builder.Property(m => m.CreatedAt)
               .HasColumnType("DATETIME")
               .IsRequired();
    }
}
