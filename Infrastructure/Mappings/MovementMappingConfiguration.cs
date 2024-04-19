using Core.Entities;
using Core.Models;
using Core.Request;
using Mapster;

namespace Infrastructure.Mappings;

public class MovementMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateMovementModel, Movement>();

              config.NewConfig<CreateMovementModel, Movement>()
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.MovementType, src => src.MovementType)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<Movement, MovementDTO>();
        config.NewConfig<CreateMovementModel, Movement>()
    .Map(dest => dest.Description, src => src.Description)
    .Map(dest => dest.MovementType, src => src.MovementType)
    .Map(dest => dest.Amount, src => src.Amount)
    .Map(dest => dest.Description, src => src.Description)
    .Map(dest => dest.Description, src => src.Description);

    }
}
