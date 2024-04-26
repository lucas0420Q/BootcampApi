using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly BootcampContext _context;

    public PaymentRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<PaymentServiceDTO> Add(CreatePaymentserviceModel model)
    {
        var validationResult = await ValidateTransactionRules(model);
        if (!validationResult.isValid)
        {
            throw new InvalidOperationException(validationResult.message);
        }

        var paymentService = model.Adapt<Payment>();
        paymentService.PaymentServiceDateTime = DateTime.UtcNow;

        _context.PaymentServices.Add(paymentService);

        var originalAccount = await _context.Accounts.FindAsync(model.AccountId);
        originalAccount.Balance -= model.Amount;

        //if (originalAccount.Type == AccountType.Current)
        //{
        //    originalAccount.CurrentAccount!.OperationalLimit -= model.Amount;
        //}

        await _context.SaveChangesAsync();

        var createPaymentService = await _context.PaymentServices
            .Include(a => a.Account)
            .ThenInclude(a => a.Currency)
            .Include(a => a.Account)
            .ThenInclude(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefaultAsync(a => a.Id == paymentService.Id);

        return createPaymentService.Adapt<PaymentServiceDTO>();
    }

    public async Task<(bool isValid, string message)> ValidateTransactionRules(CreatePaymentserviceModel model)
    {
        var originalAccount = await _context.Accounts
            .Include(a => a.Currency)
            .SingleOrDefaultAsync(a => a.Id == model.AccountId);

        if (originalAccount == null || originalAccount.Status != AccountStatus.Active)
        {
            return (false, "Invalid original account.");
        }

        if (model.Amount <= 0)
        {
            return (false, "Invalid amount.");
        }

        if (string.IsNullOrEmpty(model.DocumentNumber))
        {
            return (false, "Document number is required.");
        }

        var destinationService = await _context.Accounts.FindAsync(model.ServiceId);
        if (destinationService == null)
        {
            return (false, "Invalid destination service.");
        }

        var destinationAccount = await _context.Accounts.FindAsync(model.AccountId);
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
            return (false, "Insufficient balance.");
        }

        // No need to validate PaymentServiceDateTime as it's always DateTime.UtcNow

        return (true, "Transaction is valid.");
    }

    public async Task<Account?> FindDestinationAccount(CreatePaymentserviceModel model)
    {
        if (model.AccountId != 0)
        {
            return await _context.Accounts.FindAsync(model.AccountId);
        }

        return await _context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.CurrentAccount)
            .Include(a => a.SavingAccount)
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .SingleOrDefaultAsync(a =>
                a.Number == model.DocumentNumber &&
                a.Customer.DocumentNumber == model.DocumentNumber &&
                a.Id == model.AccountId);
    }
}