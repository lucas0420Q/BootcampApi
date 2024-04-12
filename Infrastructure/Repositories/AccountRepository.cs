using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
internal class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;

    public AccountRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<AccountDTO> Add(CreateAccountModel model)
    {
        var customer = await _context.Accounts.FindAsync(model.CustomerId);
        var currency = await _context.Accounts.FindAsync(model.CurrencyId);
        var customer2 = await _context.Customers
        .Include(c => c.Bank)
        .FirstOrDefaultAsync(c => c.Id == model.CustomerId);

        var query = _context.Accounts
       .Include(c => c.SavingAccount)
       .Include(c => c.Customers)
       .Include(c => c.Currency)
       .AsQueryable();
        var result = await query.ToListAsync();

        var accountToCreate = model.Adapt<Account>();
        _context.Accounts.Add(accountToCreate);
        await _context.SaveChangesAsync();


        if (model.Type == "Saving")
        {
            var savingAccountDTO = new CreateSavingAccountDTO
            {
                AccountId = accountToCreate.Id,
                HolderName = accountToCreate.Holder,
                SavingType = model.SavingType

            };
            var savingAccount = savingAccountDTO.Adapt<SavingAccount>();
            _context.SavingAccounts.Add(savingAccount);
        }

        else if (model.Type == "Current")
        {
            var currentAccountDTO = new CreateCurrentAccountDTO
            {
                AccountId = accountToCreate.Id,
                OperationalLimit = model.OperationalLimit,
                MonthAverage = model.MonthAverage,
                Interest = model.MonthAverage

            };
            var currentAccount = currentAccountDTO.Adapt<CurrentAccount>();
            _context.CurrentAccounts.Add(currentAccount);
        }
        else
        {
            throw new Exception("Invalid account type");
        }

        await _context.SaveChangesAsync();

        var accountDTO = accountToCreate.Adapt<AccountDTO>();
        return accountDTO;
    }
    //public async Task<AccountDTO> Add(CreateAccountModel filter)
    //{
    //    var accountToCreate = filter.Adapt<Account>();

    //    _context.Accounts.Add(accountToCreate);

    //    await _context.SaveChangesAsync();

    //    var accountDTO = accountToCreate.Adapt<AccountDTO>();

    //    return accountDTO;
    //}
    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        var account = await _context.Accounts.FindAsync(model);

        if (account is null) throw new Exception("Account was not found");

        model.Adapt(account);

        _context.Accounts.Update(account);

        await _context.SaveChangesAsync();

        var accountDTO = account.Adapt<AccountDTO>();

        return accountDTO;
    }
    public async Task<bool> Delete(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account is null) throw new Exception("Account not found");

        _context.Accounts.Remove(account);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
