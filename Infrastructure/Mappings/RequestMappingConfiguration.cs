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
        .Map(m => m.Description, r => r.Description)
        .Map(m => m.CurrencyId, r => r.CurrencyId)
        .Map(m => m.ProductId, r => r.ProductId)
        .Map(m => m.CustomerId, r => r.CustomerId);

        config.NewConfig<Request, RequestDTO>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.Currency, src => src.Currency.Name)
        .Map(dest => dest.ProductType, src => src.Product.ProductType)
        .Map(dest => dest.Customer, src => src.Customer.Name);
    }
}

