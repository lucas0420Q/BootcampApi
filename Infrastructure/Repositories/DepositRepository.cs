using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;
public class DepositRepository : IDepositRepository
{

    private readonly BootcampContext _context;

    public DepositRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<DepositDTO> Add(CreateDepositModel model)
    {
        var validationResult = await ValidateTransactionRules(model);
        if (!validationResult.isValid)
        {
            throw new InvalidOperationException(validationResult.message);
        }

        var deposit = model.Adapt<Deposit>();
        deposit.DateOperation = DateTime.UtcNow;

        _context.Deposits.Add(deposit);

        var account = await _context.Accounts
                                  .Include(a => a.Currency)
                                  .Include(a => a.CurrentAccount)
                                  .Include(a => a.SavingAccount)
                                  .Include(a => a.Customer)
                                  .ThenInclude(c => c.Bank)
                                  .SingleOrDefaultAsync(a => a.Id == model.AccountId);
        account.Balance += model.amount;



        if (account.CurrentAccount != null && model.amount > account.CurrentAccount.OperationalLimit)
        {
            throw new Exception("The operation exceeds the operational limit.");
        }

        var totalAmountOperationsTransfers = _context.Movements
                                                            .Where(t => t.OriginalAccountId == account.Id &&
                                                            t.TransferredDateTime.Value.Month == DateTime.Now.Month)
                                                            .Sum(t => t.Amount);

        var totalAmountOperationsDeposits = _context.Deposits
                                                             .Where(d => d.AccountId == account.Id &&
                                                             d.DateOperation.Month == DateTime.Now.Month)
                                                             .Sum(d => d.amount);

        var totalAmountOperationsExtractions = _context.Extractions
                                                              .Where(e => e.AccountId == account.Id &&
                                                              e.DateExtraction.Month == DateTime.Now.Month)
                                                              .Sum(e => e.amount);

        var totalAmountOperations = totalAmountOperationsTransfers + totalAmountOperationsDeposits + totalAmountOperationsExtractions;

        if ((model.amount + totalAmountOperations) > account.CurrentAccount!.OperationalLimit)
        {
            throw new Exception("Account exceeded the operational limit.");
        }

        await _context.SaveChangesAsync();

        var createDeposit = await _context.Deposits
            //.Include(a => a.Id)
            //.Include(a => a.BankId)
            .Include(a => a.Bank)
            //.Include(c => c.DateOperation)
            .FirstOrDefaultAsync(a => a.Id == deposit.Id);

        return createDeposit.Adapt<DepositDTO>();
    }
    private async Task<bool> IsDepositOperationalLimitSufficient(CurrentAccount currentAccount, decimal amount)
    {
        // Obtener el mes actual
        var currentMonth = DateTime.UtcNow.Month;

        // Calcular la suma de los depósitos del mes actual
        var totalDepositsThisMonth = await _context.Deposits
            .Where(d => d.Id == currentAccount.Id &&
                        d.DateOperation.Month == currentMonth)
            .SumAsync(d => d.amount);

        // Verificar si el límite operacional de depósitos es suficiente
        if (currentAccount.OperationalLimit.HasValue &&
            currentAccount.OperationalLimit.Value < totalDepositsThisMonth + amount)
        {
            return false;
        }

        return true;
    }
    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateDepositModel model)
    {
        // Verificar que se proporciona un ID de cuenta válido
        var depositExists = await _context.Accounts.AnyAsync(d => d.Id == model.AccountId);
        if (!depositExists)
        {
            return (false, "Deposit with the provided ID does not exist.");
        }

        // Verificar si el banco con el ID proporcionado existe en la base de datos
        var bankExists = await _context.Banks.AnyAsync(b => b.Id == model.BankId);
        if (!bankExists)
        {
            return (false, "Bank with the provided ID does not exist.");
        }

        // Verificar que el monto es mayor que cero
        if (model.amount <= 0)
        {
            return (false, "Amount must be greater than zero.");
        }

        // Si todas las validaciones pasan, devolvemos true para isValid
        return (true, "Transaction is valid.");
    }

}
