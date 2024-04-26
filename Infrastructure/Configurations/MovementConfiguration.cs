using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> entity)
    {

        entity
            .HasKey(e => e.Id)
            .HasName("Movement_pkey");
        entity
            .Property(e => e.Description)
            .HasMaxLength(100);
        entity
            .Property(e => e.Amount)
            .HasPrecision(20, 5);
        entity
            .Property(e => e.TransferredDateTime);
        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.Movements)
            .HasForeignKey(d => d.OriginalAccountId);
        entity
            .HasOne(d => d.Account)
            .WithMany(p => p.Movements)
            .HasForeignKey(d => d.DestinationAccountId);

        //entity
        //    .HasMany(Movement => Movement.Deposits)
        //    .WithOne(Deposit => Deposit.)
        //    .HasForeignKey(bank => bank.BankId);
    }
}


