using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;


public interface IAccountRepository
{
    Task<AccountDTO> Add(CreateAccountModel filter);
    Task<AccountDTO> Update(UpdateAccountModel model);
    Task<bool> Delete(int id);

}
