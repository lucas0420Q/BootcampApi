using Core.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> entity)

    {
        entity
            .HasKey(r => r.Id)
            .HasName("Request_pkey");
    
        entity
            .Property(r => r.Description)
            .IsRequired();

        //entity
        //    /*.*//*HasOne(r => r.Product)*/
        //    .WithMany(p => p.Requests)
        //    .HasForeignKey(r => r.ProductId);

        entity
            .HasOne(r => r.Currency)
            .WithMany(c => c.Requests)
            .HasForeignKey(r => r.CurrencyId);

        entity
            .HasOne(r => r.Customer)
            .WithMany(c => c.Requests)
            .HasForeignKey(r => r.CustomerId);

    }
}