using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechFoods.Kitchen.Infrastructure.Repository.Configurations;
public class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(o => o.CustomerId)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

        builder.Property(o => o.CreatedAt)
               .HasColumnType("DATETIME")
               .IsRequired();

        builder.Property(o => o.Status)
               .HasConversion<string>()
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

        builder.Property(o => o.DeliveryMethod)
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

        builder.Property(o => o.CancellationReason)
               .HasColumnType("VARCHAR(255)");

        // Relacionamento 1:N com OrderItem
        builder.HasMany(o => o.Items)
           .WithOne(oi => oi.Order) // 👈 propriedade de navegação correta
           .HasForeignKey(oi => oi.OrderId)
           .OnDelete(DeleteBehavior.Cascade);

    }
}
