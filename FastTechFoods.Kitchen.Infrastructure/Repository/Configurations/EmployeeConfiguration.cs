using FastTechFoods.Kitchen.Domain.Entities;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechFoods.Kitchen.Infrastructure.Repository.Configurations;
internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasColumnType("UNIQUEIDENTIFIER")
               .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(e => e.Name)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(e => e.Email)
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(e => e.PasswordHash)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(e => e.Role)
               .HasConversion<string>()
               .HasColumnType("VARCHAR(20)")
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .HasColumnType("DATETIME")
               .IsRequired();
    }
}
