using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IRequestService
{
    Task<RequestDTO> Add(CreateRequestModel model);
   /* T*//*ask<RequestDTO> GetById(int id);*/
}
