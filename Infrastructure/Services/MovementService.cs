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
        return await _movementRepository.Add(model);
    }
    public async Task<MovementDTO> GetById(int id)
    {
        return await _movementRepository.GetById(id);
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateMovementModel model)
    {
        return await _movementRepository.ValidateTransactionRules(model);
    }
}
