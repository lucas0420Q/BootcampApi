﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Lastname).HasPrecision(20, 5);
            entity.Property(e => e.DocumentNumber).HasMaxLength(100);
            entity.Property(e => e.Address).HasPrecision(20, 5);
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.Phone).HasPrecision(20, 5);

            entity
             .HasOne (customer => customer. Bank)
            .WithMany(Bank => Bank.Customers)
            .HasForeignKey(Bank => Bank.BankId);

        }
    }
}
