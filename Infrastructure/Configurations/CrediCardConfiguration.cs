using AngleSharp.Dom;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CrediCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            {
                entity
                    .HasKey(e => e.Id)
                    .HasName("CreditCard_pkey");

                entity
                    .Property(e => e.Designation)
                    .HasMaxLength(100)
                    .IsRequired();

                entity
                    .Property(e => e.IssueDate)
                    .HasMaxLength(100)
                    .IsRequired();

                entity
                    .Property(e => e.ExpirationDT)
                    .HasMaxLength(100)
                    .IsRequired();

                entity
                    .Property(e => e.CardNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity
                        .Property(e => e.Cvv)
                        .HasMaxLength(10)
                        .IsRequired();
                entity
                        .Property(e => e.CreditLimit)
                        .HasPrecision(20, 5)
                        .IsRequired();

                entity
                        .Property(e => e.AvailableCredit)
                        .HasPrecision(20, 5)
                        .IsRequired();
                entity
                   .Property(e => e.CurrentDebt)
                   .HasPrecision(20, 5)
                   .IsRequired();

                entity
                    .Property(e => e.InterestRate)
                    .HasPrecision(20, 5)
                    .IsRequired();


                entity
                    .HasOne(x => x.Customer)
                    .WithMany(x => x.CreditCards)
                    .HasForeignKey(x => x.CustomerId);

                entity
                    .HasOne(x => x.Currency)
                    .WithMany(x => x.CreditCards)
                    .HasForeignKey(x => x.CurrencyId);

            }
        }
    }
}