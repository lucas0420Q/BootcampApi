using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<RequestDTO> Add(CreateRequestModel model);
    //Task<RequestDTO> GetById(int id);
}
