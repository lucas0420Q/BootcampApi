using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class RequestMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRequestModel, Request>()
       //.Map(m => m.Amount, r => r.Amount)
       //.Map(m => m.Term, r => r.Term)
       // .Map(m => m.Brand, r => r.Brand)
       .Map(m => m.Description, r => r.Description)
       .Map(m => m.CurrencyId, r => r.CurrencyId)
       .Map(m => m.CustomerId, r => r.CustomerId);

        config.NewConfig<Request, RequestDTO>()
        .Map(dest => dest.Id, src => src.Id)
        //.Map(dest => dest.Amount, src => src.Amount)
        //.Map(dest => dest.Term, src => src.Term)
        //.Map(dest => dest.Brand, src => src.Brand)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.Currency, src => src.Currency.Name)
        .Map(dest => dest.Customer, src => src.Customer.Name);
    }
}

