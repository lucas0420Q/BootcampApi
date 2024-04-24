using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentServiceConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {

        entity
            .HasKey(e => e.Id)
            .HasName("PaymentService_pkey");

        entity.Property(e => e.DocumentNumber)
     .HasMaxLength(255)
     .IsRequired();

        entity.Property(e => e.Amount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        entity.Property(e => e.ServiceId)
            .IsRequired();

        entity.Property(e => e.AccountId)
            .IsRequired();

        entity.Property(e => e.PaymentServiceDateTime)
            .IsRequired();

        entity.HasOne(e => e.Account)
            .WithMany()
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

