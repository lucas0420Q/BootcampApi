using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovementRepository : IMovementRepository
{
    private readonly BootcampContext _context;

    public MovementRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<MovementDTO> Add(CreateMovementModel model)
    {
      

        // Validate the transaction rules
        var validationResult = await ValidateTransactionRules(model);
        if (!validationResult.isValid)
        {
            throw new InvalidOperationException(validationResult.message);
        }

        var movement = model.Adapt<Movement>();
        movement.TransferredDateTime = DateTime.UtcNow;
        var originalAccount = await _context.Accounts
                                  .Include(a => a.Currency)
                                  .Include(a => a.CurrentAccount)
                                  .Include(a => a.SavingAccount)
                                  .Include(a => a.Customer)
                                  .ThenInclude(c => c.Bank)
                                  .SingleOrDefaultAsync(a => a.Id == model.OriginalAccountId);


        var destinationAccount = await _context.Accounts
                                 .Include(a => a.CurrentAccount)
                                 .Include(a => a.SavingAccount)
                                 .Include(a => a.Customer)
                                 .ThenInclude(c => c.Bank)
                                 .FirstOrDefaultAsync(a => a.Id == model.DestinationAccountId);

        if (originalAccount.CurrentAccount != null && model.Amount > originalAccount.CurrentAccount.OperationalLimit)
        {
            throw new Exception("The operation exceeds the operational limit.");
        }

        var totalAmountOperationsOATransfers = _context.Movements
                                                            .Where(t => t.OriginalAccountId == originalAccount.Id &&
                                                            t.TransferredDateTime.Value.Month == DateTime.Now.Month)
                                                            .Sum(t => t.Amount);

        var totalAmountOperationsOADeposits = _context.Deposits
                                                             .Where(d => d.AccountId == originalAccount.Id &&
                                                             d.DateOperation.Month == DateTime.Now.Month)
                                                             .Sum(d => d.amount);

        var totalAmountOperationsOAExtractions = _context.Extractions
                                                              .Where(e => e.AccountId == originalAccount.Id &&
                                                              e.DateExtraction.Month == DateTime.Now.Month)
                                                              .Sum(e => e.amount);

        var totalAmountOperationsOA = totalAmountOperationsOATransfers + totalAmountOperationsOADeposits + totalAmountOperationsOAExtractions;

        if ((model.Amount + totalAmountOperationsOA) > originalAccount.CurrentAccount!.OperationalLimit)
        {
            throw new Exception("OriginAccount exceeded the operational limit.");
        }

        var totalAmountOperationsDATransfers = _context.Movements
                                                  .Where(t => t.DestinationAccountId == destinationAccount.Id &&
                                                  t.TransferredDateTime.Value.Month == DateTime.Now.Month)
                                                  .Sum(t => t.Amount);

        var totalAmountOperationsDADeposits = _context.Deposits
                                                  .Where(d => d.AccountId == destinationAccount.Id &&
                                                  d.DateOperation.Month == DateTime.Now.Month)
                                                  .Sum(d => d.amount);

        var totalAmountOperationsDAExtractions = _context.Extractions
                                                  .Where(e => e.AccountId == destinationAccount.Id &&
                                                  e.DateExtraction.Month == DateTime.Now.Month)
                                                  .Sum(e => e.amount);

        var totalAmountOperationsDA = totalAmountOperationsDATransfers + totalAmountOperationsDADeposits + totalAmountOperationsDAExtractions;

        if ((model.Amount + totalAmountOperationsDA) > destinationAccount.CurrentAccount!.OperationalLimit)
        {
            throw new Exception("DestinationAccount exceeded the operational limit.");
        }

        // Add the movement to the database
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        // Update the balances of the accounts
        //var originalAccount = await _context.Accounts.FindAsync(model.OriginalAccountId);
        originalAccount.Balance -= model.Amount;
        //var destinationAccount = await FindDestinationAccount(model);
        destinationAccount.Balance += model.Amount;

        await _context.SaveChangesAsync();

        // Map the movement to a DTO and return it
        var createdMovement = await _context.Movements
            .Include(a => a.Account)
            .ThenInclude(a => a.Currency)
            .Include(a => a.Account)
            .ThenInclude(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == movement.Id);
        return createdMovement.Adapt<MovementDTO>();
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateMovementModel model)
    {
        //Validate the original account
        var originalAccount = await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .SingleOrDefaultAsync(a => a.Id == model.OriginalAccountId);

        if (originalAccount == null || originalAccount.Status != AccountStatus.Active)
        {
            return (false, "Invalid original account.");
        }
        // Validate the destination account
        var destinationAccount = await _context.Accounts
                                               .Include(a => a.CurrentAccount)
                                               .Include(a => a.SavingAccount)
                                               .Include(a => a.Customer)
                                               .Include(a => a.Customer)
                                               .ThenInclude(c => c.Bank)
                                               .FirstOrDefaultAsync(a => a.Id == model.DestinationAccountId);
        if (destinationAccount == null)
        {
            return (false, "Invalid destination account.");
        }

        if (originalAccount.Type != destinationAccount.Type)
        {
            return (false, "Incompatible account types.");
        }

        if (originalAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            return (false, "Incompatible currencies.");
        }
        if (originalAccount.Balance < model.Amount)
        {
            return (false, "insufficient balance.");
        }

        // Validate the amount
        if (model.Amount <= 0)
        {
            return (false, "Invalid amount.");
        }
        // Validate the transfer date and time
        if (model.TransferredDateTime == null)
        {
            return (false, "Invalid transfer date and time.");
        }
        // Validate the destination bank (if applicable)
        if (originalAccount.Customer != null && originalAccount.Customer.Bank.Id
            == model.DestinationBankId)
        {
            if (string.IsNullOrEmpty(model.DestinationAccountNumber) ||
                string.IsNullOrEmpty(model.DestinationAccountNumber) ||
                model.DestinationBankId == 0)
            {
                return (false, "Document number, account number, and destiny bank ID " +
                    "are required when transferring within the same bank.");
            }
        }
        await _context.SaveChangesAsync();
        return (true, "Transaction is valid.");
    }

    //busca la cuenta destino utilizando el número de cuenta, el nro ci.
    //Si se proporciona un Id de cuenta destino, se devuelve directamente esa cuenta.
    //Si no se encuentra la cuenta destino, devuelve null.
    public async Task<Account?> FindDestinationAccount(CreateMovementModel model)
    {
        // If the destination account ID is provided, use it
        if (model.DestinationAccountId != 0)
        {
            return await _context.Accounts.FindAsync(model.DestinationAccountId);
        }
        // Otherwise, search for the destination account using the provided data
        return await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .SingleOrDefaultAsync(a =>
            a.Number == model.DestinationAccountNumber &&
            a.Customer.DocumentNumber == model.DestinationDocumentNumber &&
            a.CurrencyId == model.CurrencyId &&
            a.Customer.Bank.Id == model.DestinationBankId);
    }
    public async Task<MovementDTO> GetById(int id)
    {
        var movement = await _context.Movements
            .Include(a => a.Account)
            .ThenInclude(a => a.Currency)
            .Include(a => a.Account)
            .ThenInclude(a => a.CurrentAccount)
            .Include(a => a.Account)
            .ThenInclude(a => a.SavingAccount)
            .Include(a => a.Account)
            .ThenInclude(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movement == null)
        {
            throw new NotFoundException($"Movement with ID {id} not found.");
        }

        return movement.Adapt<MovementDTO>();
    }
}
