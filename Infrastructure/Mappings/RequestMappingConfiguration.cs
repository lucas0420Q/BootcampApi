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
        .Map(m => m.RequestDate, r => r.RequestDate)
        .Map(m => m.ApprovalDate, r => r.ApprovalDate)
        .Map(m => m.CurrencyId, r => r.CurrencyId)
        .Map(m => m.Status, r => r.Status)
        .Map(m => m.CustomerId, r => r.CustomerId);

        config.NewConfig<Request, RequestDTO>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Description, src => src.Description)
        .Map(m => m.RequestDate, r => r.RequestDate)
        .Map(m => m.ApprovalDate, r => r.ApprovalDate)
        .Map(dest => dest.Currency, src => src.Currency.Name)
        .Map(dest => dest.Status, src => src.Status.ToString()) 
        .Map(dest => dest.Customer, src => src.Customer.Name);
    }
}

