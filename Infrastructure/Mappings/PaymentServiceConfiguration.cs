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
          .Map(dest => dest.ServiceId, src => src.ServiceId)
          .Map(dest => dest.AccountId, src => src.AccountId)
          .Map(dest => dest.PaymentServiceDateTime, src => src.PaymentServiceDateTime);

        config.NewConfig<Payment, PaymentServiceDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.DocumentNumber, src => src.DocumentNumber)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.ServiceId, src => src.ServiceId)
            .Map(dest => dest.Account, src => src.Account)
            .Map(dest => dest.PaymentServiceDateTime, src => src.PaymentServiceDateTime);
    }
}

