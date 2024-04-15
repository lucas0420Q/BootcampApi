using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Requests;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<AccountDTO> Create(CreateAccountRequest request)
        => await _repository.Create(request);

    public async Task<AccountDTO> GetById(int id) => await _repository.GetById(id);
}
