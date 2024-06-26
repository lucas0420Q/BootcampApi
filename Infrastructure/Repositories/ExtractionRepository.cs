﻿using Core.Constants;
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
    public class ExtractionRepository : IExtractionRepository
    {
        private readonly BootcampContext _context;

        public ExtractionRepository(BootcampContext context)
        {
            _context = context;
        }

        public async Task<ExtractionDTO> Add(CreateExtractionModel model)
        {
            var validationResult = await ValidateTransactionRules(model);
            if (!validationResult.isValid)
            {
                throw new InvalidOperationException(validationResult.message);
            }

            var extraction = model.Adapt<Extraction>(); // Crear una instancia de Extraction
            extraction.DateExtraction = DateTime.UtcNow;

            _context.Extractions.Add(extraction); // Agregar la extracción al contexto

            var account = await _context.Accounts
                                  .Include(a => a.Currency)
                                  .Include(a => a.CurrentAccount)
                                  .Include(a => a.SavingAccount)
                                  .Include(a => a.Customer)
                                  .ThenInclude(c => c.Bank)
                                  .SingleOrDefaultAsync(a => a.Id == model.AccountId);

            if(account.Balance < model.amount)
            {
                throw new Exception("Insufficient Balance");
            }

            account.Balance -= model.amount;



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

            var createExtraction = await _context.Extractions
                .FirstOrDefaultAsync(a => a.Id == extraction.Id);

            return createExtraction.Adapt<ExtractionDTO>(); // Adaptar y devolver la extracción creada como DTO
        }

        public async Task<(bool isValid, string message)> ValidateTransactionRules(CreateExtractionModel model)
        {
            // Verificar que se proporciona un ID de cuenta válido
            var ExtractionExists = await _context.Accounts.AnyAsync(d => d.Id == model.AccountId);
            if (!ExtractionExists)
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




            //// Verificar si se sobrepasa el límite operacional
            //var originalAccount = await _context.Accounts
            //    .Include(a => a.CurrentAccount)
            //    .SingleOrDefaultAsync(a => a.Id == model.AccountId);

            //if (originalAccount == null)
            //{
            //    return (false, "Invalid original account.");
            //}

            //// Verificar el límite operacional si la cuenta es de tipo "Current"
            //if (originalAccount.Type == AccountType.Current &&
            //    originalAccount.CurrentAccount != null &&
            //    model.amount > originalAccount.CurrentAccount.OperationalLimit)
            //{
            //    return (false, "Amount exceeds the operational limit.");
            //}

            //// Si todas las validaciones pasan, devolvemos true para isValid
            //return (true, "Transaction is valid.");
        }
    }
}
