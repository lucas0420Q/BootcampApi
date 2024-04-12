using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDTO> Add(CreateAccountModel filter)
    {
        return await _accountRepository.Add(filter);
    }
    public async Task<AccountDTO> Update(UpdateAccountModel filter)
    {
        return await _accountRepository.Update(filter);
    }
    public async Task<bool> Delete(int id)
    {
        return await _accountRepository.Delete(id);
    }

}
