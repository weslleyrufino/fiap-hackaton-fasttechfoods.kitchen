using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechFoods.Kitchen.Infrastructure.Repository.Configurations;
public class ContatosConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("UNIQUEIDENTIFIER")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.Name)
            .HasColumnName("Nome")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(p => p.Telefone)
            .HasColumnName("Telefone")
            .HasColumnType("VARCHAR(20)")
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.HasOne(p => p.Regiao)
               .WithMany()
               .HasForeignKey(p => p.RegiaoId)
               .OnDelete(DeleteBehavior.Restrict); // Impede de excluir uma Regiao se houver Contatos que estejam usando essa Regiao.
    }
}
