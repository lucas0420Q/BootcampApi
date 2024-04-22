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

        // Validar las reglas de negocio antes de agregar el movimiento
        await ValidateTransactionRules(model);

        var movement = model.Adapt<Movement>();
        movement.TransferredDateTime = DateTime.UtcNow;

        // Agregar el movimiento a la base de datos
        _context.Movements.Add(movement);
        await _context.SaveChangesAsync();

        // Actualizar los saldos de las cuentas
        var originalAccount = await _context.Accounts.FindAsync(model.OriginalAccountId);
        originalAccount.Balance -= model.Amount;

        var destinationAccount = await FindDestinationAccount(model);
        destinationAccount.Balance += model.Amount;

        await _context.SaveChangesAsync();

        return model.Adapt<MovementDTO>();
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateMovementModel model)
    {
        var originalAccount = await _context.Accounts
            .Include(a => a.Currency)
            .SingleOrDefaultAsync(a => a.Id == model.OriginalAccountId);

        if (originalAccount == null || originalAccount.Status != AccountStatus.Active)
        {
            throw new BusinessLogicException("Invalid original account.");
        }

        if (originalAccount.Customer != null && originalAccount.Customer.Bank.Id == model.DestinationBankId)
        {
            if (string.IsNullOrEmpty(model.DestinationAccountNumber) || string.IsNullOrEmpty
                (model.DestinationAccountNumber) || model.DestinationBankId == 0)
            {
                throw new BusinessLogicException
                ("Document number, account number, and destiny bank ID are required when transferring within the same bank.");
            }
        }

        if (model.Amount > originalAccount.Balance)
        {
            throw new BusinessLogicException("Insufficient balance.");
        }

        var destinationAccount = await FindDestinationAccount(model);

        if (destinationAccount == null)
        {
            throw new BusinessLogicException("Invalid destination account.");
        }

        if (originalAccount.Type != destinationAccount.Type)
        {
            throw new BusinessLogicException("Incompatible account types.");
        }

        if (originalAccount.CurrencyId != destinationAccount.CurrencyId)
        {
            throw new BusinessLogicException("Incompatible currencies.");
        }

        //if (originalAccount.Type == AccountType.Current && originalAccount.CurrentAccount!.OperationalLimit < model.Amount)
        if (originalAccount != null &&
    originalAccount.Type == AccountType.Current &&
    originalAccount.CurrentAccount != null &&
    originalAccount.CurrentAccount.OperationalLimit < model.Amount)
        {
            throw new ArgumentException("Operational limit exceeded.");
        }
        return (true, "Todo bien");
    }

    //busca la cuenta destino utilizando el número de cuenta, el nro ci.
    //Si se proporciona un Id de cuenta destino, se devuelve directamente esa cuenta.
    //Si no se encuentra la cuenta destino, devuelve null.
    private async Task<Account?> FindDestinationAccount(CreateMovementModel model)
    {
        // If the destination account ID is provided, use it
        if (model.DestinationAccountId != 0)
        {
            return await _context.Accounts.FindAsync(model.DestinationAccountId);
        }

        // Otherwise, search for the destination account using the provided data
        return await _context.Accounts
            .Include(a => a.Currency)
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
        throw new NotImplementedException();

    }
}