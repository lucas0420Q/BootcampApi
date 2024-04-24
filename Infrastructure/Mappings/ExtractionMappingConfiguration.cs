using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class ExtractionMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateExtractionModel, Extraction>()
           .Map(dest => dest.BankId, src => src.BankId)
           .Map(dest => dest.amount, src => src.amount)
           .Map(dest => dest.DateExtraction, src => src.DateExtraction);

        config.NewConfig<Extraction, ExtractionDTO>()
           .Map(dest => dest.Id, src => src.Id)
           .Map(dest => dest.BankId, src => src.BankId)
           .Map(dest => dest.Bank, src => src.Bank)
           .Map(dest => dest.amount, src => src.amount)
           .Map(dest => dest.DateExtraction, src => src.DateExtraction);
    }
}
