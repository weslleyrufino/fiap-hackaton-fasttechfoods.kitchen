using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechFoods.Kitchen.Infrastructure.Repository.Configurations;
public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
               .HasColumnType("UNIQUEIDENTIFIER")
               .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(oi => oi.MenuItemId)
               .HasColumnType("UNIQUEIDENTIFIER")
               .IsRequired();

        builder.Property(oi => oi.OrderId) // 👈 Aqui está o OrderId
               .HasColumnType("UNIQUEIDENTIFIER")
               .IsRequired();

        builder.Property(oi => oi.Quantity)
               .HasColumnType("INT")
               .IsRequired();

        builder.Property(oi => oi.UnitPrice)
               .HasColumnType("DECIMAL(10,2)")
               .IsRequired();

        builder.Ignore(oi => oi.Total); // calculado em memória

        // 🔗 Relacionamento com Order (1:N)
        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.Items)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        // 🔗 Relacionamento com MenuItem (N:1)
        builder.HasOne<MenuItem>()
               .WithMany()
               .HasForeignKey(oi => oi.MenuItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

