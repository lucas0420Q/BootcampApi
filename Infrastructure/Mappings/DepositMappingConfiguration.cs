using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class DepositMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
          config.NewConfig<CreateDepositModel, Deposit>()
         .Map(dest => dest.BankId, src => src.BankId)
         .Map(dest => dest.amount, src => src.amount)
         .Map(dest => dest.DateOperation, src => src.DateOperation);

            config.NewConfig<Deposit, DepositDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.BankId, src => src.BankId)
            .Map(dest => dest.Bank, src => src.Bank)
            .Map(dest => dest.amount, src => src.amount)
            .Map(dest => dest.DateOperation, src => src.DateOperation);
        }
    }
}
