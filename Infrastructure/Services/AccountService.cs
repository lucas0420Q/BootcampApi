﻿using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<AccountDTO> Add(CreateAccountRequest model)
    {
        return await _accountRepository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _accountRepository.Delete(id);
    }

    public async Task<AccountDTO> GetById(int id)
    {
        return await _accountRepository.GetById(id);
    }

    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        return await _accountRepository.GetFiltered(filter);
    }

    public Task<List<AccountDTO>> GetFiltered(FilterTransactionsAccount filter)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        return await _accountRepository.Update(model);
    }
}