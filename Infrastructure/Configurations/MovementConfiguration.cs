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
            .HasName("Movements_pkey");

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
    }
}

        //entity.Property(e => e.Destination).HasMaxLength(400);
        //entity.Property(e => e.Amount).HasMaxLength(100);
        //entity.Property(e => e.TransferredDateTime).HasMaxLength(300);
        //entity.Property(e => e.TransferStatus).HasMaxLength(150);

        //// Relaciones
        ////entity.HasOne(e => e.Account)
        ////    .WithMany(a => a.Movements)
        ////    .HasForeignKey(e => e.Account);
        //entity.HasOne(m => m.Account)
        //    .WithMany() // o WithOne si es una relación uno a uno
        //    .HasForeignKey(m => m.OriginalAccountId); // Nombre de la clave foránea

        //entity.HasOne(e => e.OriginalAccount)
        //.WithMany()
        //.HasForeignKey(e => e.OriginalAccountId);

        //entity.HasOne(e => e.DestinationAccount)
        //    .WithMany()
        //    .HasForeignKey(e => e.DestinationAccountId);


