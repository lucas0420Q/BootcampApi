using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IRequestService
{
    Task<RequestDTO> Add(CreateRequestModel model);
    Task<RequestDTO> GetById(int id);
}
