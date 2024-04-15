using Core.Models;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<AccountDTO> Create(CreateAccountRequest request);
    Task<AccountDTO> GetById(int id);
}
