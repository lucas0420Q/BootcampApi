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
        //entity.Property(e => e.BankId).IsRequired();
        entity.Property(e => e.amount).HasColumnType("decimal(18, 2)").IsRequired();
        entity.Property(e => e.DateExtraction).IsRequired();

        // Definir la relación con la entidad BankDTO
        entity
              .HasOne(Extraction => Extraction.Bank)
              .WithMany(Bank => Bank.Extractions)
              .HasForeignKey(Extraction => Extraction.BankId);

        entity
                .HasOne(Extraction => Extraction.Account)
                .WithMany(Account => Account.Extractions)
                .HasForeignKey(Extraction => Extraction.AccountId);





    }
}
