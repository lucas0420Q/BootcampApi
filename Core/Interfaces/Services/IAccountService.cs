using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services
{

    public interface IAccountService
    {
        Task<AccountDTO> Add(CreateAccountModel filter);
        Task<AccountDTO> Update(UpdateAccountModel filter);
        Task<bool> Delete(int id);
    }
}

