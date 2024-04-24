using Core.Models;
using Core.Request;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter);
    Task<AccountDTO> GetById(int id);
    Task<AccountDTO> Add(CreateAccountRequest model);
    Task<AccountDTO> Update(UpdateAccountModel model);
    Task<bool> Delete(int id);
    Task<List<AccountDTO>> GetFiltered(FilterTransactionsAccount filter);
}
