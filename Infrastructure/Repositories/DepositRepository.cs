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

        var Deposit = model.Adapt<Deposit>();
        Deposit.DateOperation = DateTime.UtcNow;

        _context.Deposits.Add(Deposit);

        var originalAccount = await _context.Accounts.FindAsync(model.AccountId);
        originalAccount.Balance += model.amount;

        if (originalAccount.Type == AccountType.Current)
        {
            originalAccount.CurrentAccount!.OperationalLimit += model.amount;
        }

        await _context.SaveChangesAsync();

        var createDeposit = await _context.Deposits
            //.Include(a => a.Id)
            //.Include(a => a.BankId)
            .Include(a => a.Bank)
            //.Include(c => c.DateOperation)
            .FirstOrDefaultAsync(a => a.Id == Deposit.Id);

        return createDeposit.Adapt<DepositDTO>();

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

        // Verificar si se sobrepasa el límite operacional
        var originalAccount = await _context.Accounts
            .Include(a => a.CurrentAccount)
            .SingleOrDefaultAsync(a => a.Id == model.AccountId);

        if (originalAccount == null)
        {
            return (false, "Invalid original account.");
        }

        // Verificar el límite operacional si la cuenta es de tipo "Current"
        if (originalAccount.Type == AccountType.Current &&
            originalAccount.CurrentAccount != null &&
            model.amount > originalAccount.CurrentAccount.OperationalLimit)
        {
            return (false, "Amount exceeds the operational limit.");
        }

        // Si todas las validaciones pasan, devolvemos true para isValid
        return (true, "Transaction is valid.");
    }
}