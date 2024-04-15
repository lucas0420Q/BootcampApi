using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    Task<AccountDTO> Create(CreateAccountRequest request);
    Task<AccountDTO> GetById(int id);
}

