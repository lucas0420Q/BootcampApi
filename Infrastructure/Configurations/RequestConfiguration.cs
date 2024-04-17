using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Configurations
{
    internal class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> entity)
        {
            // Nombre de la tabla
            entity.ToTable("Requests");

            // Clave primaria
            entity.HasKey(r => r.Id);

            // Propiedad Name
            entity.Property(r => r.Name)
                   .IsRequired();

            // Propiedad RequestDate
            entity.Property(r => r.RequestDate)
                   .IsRequired();

            // Propiedad BankApprovalDate
            entity.Property(r => r.BankApprovalDate);

            // Relación con la entidad Product
            entity.HasOne(r => r.Product)
                   .WithMany()
                   .HasForeignKey(r => r.ProductId)
                   .IsRequired();
             /*      .OnDelete(DeleteBehavior.Restrict); */// Opcional: Define el comportamiento de eliminación
        }
    }
}
/*      .OnDelete(DeleteBehavior.Restrict); */// Opcional: Define el comportamiento de eliminación
