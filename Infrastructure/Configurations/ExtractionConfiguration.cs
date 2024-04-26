using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExtractionConfiguration : IEntityTypeConfiguration<Extraction>
{
    public void Configure(EntityTypeBuilder<Extraction> entity)
    {
        entity
                .HasKey(e => e.Id);
        //entity.Property(e => e.Id).IsRequired();
        entity.Property(e => e.BankId).IsRequired();
        entity.Property(e => e.amount).HasColumnType("decimal(18, 2)").IsRequired();
        entity.Property(e => e.DateExtraction).IsRequired();

        // Definir la relación con la entidad BankDTO
        entity.HasOne(e => e.Bank)
               .WithMany()
               .HasForeignKey(e => e.BankId);

        entity.HasOne(e => e.Account)
               .WithMany()
               .HasForeignKey(e => e.AccountId);





    }
}
