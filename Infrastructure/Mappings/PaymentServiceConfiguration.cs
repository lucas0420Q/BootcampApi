using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class PaymentServiceConfiguration : IRegister

{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePaymentserviceModel, Payment>()
        .Map(dest => dest.DocumentNumber, src => src.DocumentNumber)
        .Map(dest => dest.Amount, src => src.Amount)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.AccountId, src => src.AccountId)
        .Map(dest => dest.PaymentServiceDateTime, src => src.PaymentServiceDateTime);

        config.NewConfig<Payment, PaymentServiceDTO>()
            .Map(dest => dest.DocumentNumber, src => src.DocumentNumber)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AccountId, src => src.AccountId)
            .Map(dest => dest.PaymentServiceDateTime, src => src.PaymentServiceDateTime)
            .Map(dest => dest.Account, src => src.Account);
    }
}

