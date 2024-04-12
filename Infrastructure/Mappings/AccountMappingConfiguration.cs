using Core.Constants;
using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class AccountMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<CreateAccountModel, Account>()
                .Map(dest => dest.Holder, src => src.Holder)
                .Map(dest => dest.Number, src => src.Number)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.CurrencyId, src => src.CurrencyId)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.SavingAccount, src => src.SavingAccount)
                .Map(dest => dest.CurrentAccount, src => src.CurrentAccount);

        config.NewConfig<Account, AccountDTO>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Holder, src => src.Holder)
                .Map(dest => dest.Number, src => src.Number)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.CurrencyId, src => src.CurrencyId)
                .Map(dest => dest.CustomerId, src => src.CustomerId)
                .Map(dest => dest.IsDelete, src => src.IsDelete)
                .Map(dest => dest.SavingAccount, src => src.SavingAccount)
                .Map(dest => dest.CurrentAccount, src => src.CurrentAccount);
    }

}