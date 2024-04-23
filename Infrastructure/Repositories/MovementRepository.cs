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
        // Add the movement to the database
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();
        // Update the balances of the accounts
        var originalAccount = await _context.Accounts.FindAsync(model.OriginalAccountId);
        originalAccount.Balance -= model.Amount;
        var destinationAccount = await FindDestinationAccount(model);
        destinationAccount.Balance += model.Amount;

        if (originalAccount.Type == AccountType.Current)
        {
            originalAccount.CurrentAccount!.OperationalLimit -= model.Amount;
        }
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
        // Validate the original account
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
        var destinationAccount = await FindDestinationAccount(model);
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

        // Validate the operational limit (if applicable)
        if (originalAccount.Type == AccountType.Current)
        {
            var currentAccount = originalAccount.CurrentAccount;
            var currentMonth = DateTime.UtcNow.Month;
            var totalTransfersThisMonth = await _context.Movements
                .Where(m => m.OriginalAccountId == originalAccount.Id && m.TransferredDateTime.HasValue && m.TransferredDateTime.Value.Month == currentMonth)
                .SumAsync(m => m.Amount);
            var remainingOperationalLimit = currentAccount.OperationalLimit - totalTransfersThisMonth;
            if (remainingOperationalLimit < model.Amount)
            {
                return (false, "The transfer amount exceeds the operational limit.");
            }
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

    public async Task<MovementDTO> Add(CreatePaymentserviceModel model)
    {
        // Validate the transaction rules
        var validationResult = await ValidateTransactionRules(model);
        if (!validationResult.isValid)
        {
            throw new InvalidOperationException(validationResult.message);
        }

        // Create the movement entity
        var movement = model.Adapt<Movement>();
        movement.TransferredDateTime = DateTime.UtcNow;

        // Add the movement to the database
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        // Update the balances of the accounts
        var originalAccount = await _context.Accounts.FindAsync(model.AccountId);
        originalAccount.Balance -= model.Amount;

        //var destinationAccount = await FindDestinationAccount(model);
        //destinationAccount.Balance += model.Amount;

        if (originalAccount.Type == AccountType.Current)
        {
            originalAccount.CurrentAccount!.OperationalLimit -= model.Amount;
        }

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

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreatePaymentserviceModel model)
    {
        // Validar la cuenta original
        var originalAccount = await _context.Accounts
            .Include(a => a.Currency)
            .SingleOrDefaultAsync(a => a.Id == model.AccountId);

        if (originalAccount == null || originalAccount.Status != AccountStatus.Active)
        {
            return (false, "Invalid original account.");
        }

        // Validar el monto
        if (model.Amount <= 0)
        {
            return (false, "Invalid amount.");
        }

        // Validar el número de documento
        if (string.IsNullOrEmpty(model.DocumentNumber))
        {
            return (false, "Document number is required.");
        }

        // Validar la descripción
        if (string.IsNullOrEmpty(model.Description))
        {
            return (false, "Description is required.");
        }

        // Validar la existencia de la cuenta
        var destinationAccount = await _context.Accounts.FindAsync(model.AccountId);
        if (destinationAccount == null)
        {
            return (false, "Invalid destination account.");
        }

        // Validar tipos de cuenta compatibles
        if (originalAccount.Type != destinationAccount.Type)
        {
            return (false, "Incompatible account types.");
        }

        // Validar monedas compatibles
        if (originalAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            return (false, "Incompatible currencies.");
        }

        // Validar saldo suficiente
        if (originalAccount.Balance < model.Amount)
        {
            return (false, "Insufficient balance.");
        }

        // Validar fecha y hora de transferencia
        if (model.PaymentServiceDateTime == null)
        {
            return (false, "Invalid transfer date and time.");
        }

        await _context.SaveChangesAsync();
        return (true, "Transaction is valid.");
    }

    public async Task<Account?> FindDestinationAccount(CreatePaymentserviceModel model)
    {
        // If the destination account ID is provided, use it
        if (model.AccountId != 0)
        {
            return await _context.Accounts.FindAsync(model.AccountId);
        }

        // Otherwise, search for the destination account using the provided data
        return await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .SingleOrDefaultAsync(a =>
                a.Number == model.Account.Number &&
                a.Customer.DocumentNumber == model.DocumentNumber &&
                a.Customer.Bank.Id == model.Account.Id);
    }
}
