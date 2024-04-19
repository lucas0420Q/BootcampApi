using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IMovementService
{
    Task<MovementDTO> GetById(int id);
    Task<MovementDTO> Add(CreateMovementModel model);
}
