﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> entity)
        {
            entity.ToTable("Bank");

            entity.HasKey(e => e.Id).HasName("Bank_pkey");

            entity.Property(e => e.Name).HasMaxLength(400).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Mail).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(150).IsRequired();

            entity
            .HasMany(bank => bank.Customers)
            .WithOne(customer => customer.Bank)
            .HasForeignKey(Customer => Customer.BankId);

            entity
            .HasMany(bank => bank.Deposits)
            .WithOne(Deposit => Deposit.Bank)
            .HasForeignKey(bank => bank.BankId);

            entity
            .HasMany(bank => bank.Extractions)
            .WithOne(extraction => extraction.Bank)
            .HasForeignKey(extraction => extraction.BankId);

        }
    }
}
