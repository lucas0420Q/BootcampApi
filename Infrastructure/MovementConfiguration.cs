using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> entity)
        {

            entity.ToTable("Movement");

            entity.HasKey(e => e.Id).HasName("Movemen_pkey");

            entity.Property(e => e.Destination).HasMaxLength(400).IsRequired();
            entity.Property(e => e.Amount).HasMaxLength(100).IsRequired();
            entity.Property(e => e.TransferredDateTime).HasMaxLength(300).IsRequired();
            entity.Property(e => e.TransferStatus).HasMaxLength(150).IsRequired();

            entity
            .HasOne(Movement => Movement.Account)
            .WithMany(Account => Account.Movements)
            .HasForeignKey(Account => Account.AccountId);
        }
    }
}

