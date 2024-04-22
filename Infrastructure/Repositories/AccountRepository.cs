
using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BootcampContext _context;
    public AccountRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<AccountDTO> Add(CreateAccountRequest model)
    {

        var account = model.Adapt<Account>();

        if (account.Type == AccountType.Saving)
        {
            account.SavingAccount = model.CreateSavingAccount.Adapt<SavingAccount>();
        }

        if (account.Type == AccountType.Current)
        {
            account.CurrentAccount = model.CreateCurrentAccount.Adapt<CurrentAccount>();
        }

        _context.Accounts.Add(account);

        await _context.SaveChangesAsync();

        var createdAccount = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.Customer)
            .Include(a => a.SavingAccount)
            .Include(a => a.CurrentAccount)
            .FirstOrDefaultAsync(a => a.Id == account.Id);

        return createdAccount.Adapt<AccountDTO>();
    }
    public async Task<AccountDTO> GetById(int id)
    {
        var account = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .Include(a => a.SavingAccount)
            .Include(a => a.CurrentAccount)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (account is null) throw new NotFoundException($"The account with id: {id} doest not exist");

        return account.Adapt<AccountDTO>();
    }
    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        var account = await _context.Accounts.FindAsync(model.Id);

        if (account is null) throw new Exception("Account was not found");
        model.Adapt(account);
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        //var accountDTO = account.Adapt<AccountDTO>();
        var updateAccount = await _context.Accounts
        .Include(a => a.Currency)
        .Include(a => a.Customer)
        .ThenInclude(c => c.Bank)
        .Include(a => a.SavingAccount)
        .Include(a => a.CurrentAccount)
        .FirstOrDefaultAsync(a => a.Id == account.Id);
        return updateAccount.Adapt<AccountDTO>();
    }
    public async Task<bool> Delete(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account is null) throw new NotFoundException("Account with ID " + id + " was not found");
        account.IsDeleted = IsDeletedStatus.True;
        //account.Status = AccountStatus.Inactive;
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {

        var query = _context.Accounts
                   .Include(a => a.Currency)
                   .Include(a => a.Customer)
                   .ThenInclude(a => a.Bank)
                   .Include(a => a.SavingAccount)
                   .Include(a => a.CurrentAccount)
                   .AsQueryable();

        if (filter.CurrencyId is not null)
        {
            query = query.Where(x =>
                x.Currency != null &&
                x.CurrencyId == filter.CurrencyId);
        }
        if (filter.Type.HasValue)
        {
            query = query.Where(x =>
                x.Type == filter.Type.Value);
        }

        if (filter.Number is not null)
        {
            query = query.Where(x =>
                x.Number != null &&
                x.Number.Equals(filter.Number));
        }

        var result = await query.ToListAsync();
        var accountDTO = result.Adapt<List<AccountDTO>>();
        return accountDTO;
    }

}

