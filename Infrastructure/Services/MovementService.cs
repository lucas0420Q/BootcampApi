using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;

namespace Infrastructure.Services;

public class MovementeService : IMovementService
{
    private readonly IMovementRepository _movementRepository;

    public MovementeService(IMovementRepository movementRepository)
    {
        _movementRepository = movementRepository;
    }

    public async Task<MovementDTO> Add(CreateMovementModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<MovementDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }
}