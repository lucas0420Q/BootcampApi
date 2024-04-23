using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
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

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
    private readonly BootcampContext _context;

        public PaymentRepository(BootcampContext context)
        {
            _context = context;
        }
        public async Task<PaymentServiceDTO> Add(CreatePaymentserviceModel model)
        {
            // Validate the transaction rules
            var validationResult = await ValidateTransactionRules(model);
            if (!validationResult.isValid)
            {
                throw new InvalidOperationException(validationResult.message);
            }

            // Create the movement entity
            var paymentService = model.Adapt < Payment>();
                paymentService.PaymentServiceDateTime = DateTime.UtcNow;

                // Add the movement to the database
                _context.PaymentServices.Add(paymentService);
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

}
