using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IMovementRepository
{
    Task<MovementDTO> GetById(int id);
    Task<MovementDTO> Add(CreateMovementModel model);

}
