using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> entity)
        {
            entity
                 .HasKey(e => e.Id);
            //entity.Property(e => e.Id).IsRequired();
            //entity.Property(e => e.AccountId).IsRequired();
            //entity.Property(e => e.BankId).IsRequired();
            entity.Property(e => e.amount).HasColumnType("decimal(18, 2)").IsRequired();
            entity.Property(e => e.DateOperation).IsRequired();

            // Definir la relación con la entidad BankDTO
            entity
              .HasOne(Deposit => Deposit.Bank)
              .WithMany(Bank => Bank.Deposits)
              .HasForeignKey(Deposit => Deposit.BankId);

            entity
              .HasOne(Deposit => Deposit.Account)
              .WithMany(account => account.Deposits)
              .HasForeignKey(Deposit => Deposit.AccountId);
        }
    }
}
