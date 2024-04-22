using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class MovementMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMovementModel, Movement>()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.OriginalAccountId, src => src.OriginalAccountId)
            .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId);
        config.NewConfig<Movement, MovementDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.TransferredDateTime, src => src.TransferredDateTime)
            .Map(dest => dest.TransferStatus, src => src.TransferStatus)
            .Map(dest => dest.OriginalAccountId, src => src.OriginalAccountId)
            .Map(dest => dest.DestinationAccountId, src => src.DestinationAccountId);

    }
}
