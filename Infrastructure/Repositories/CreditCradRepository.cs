using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly BootcampContext _context;

        public CreditCardRepository(BootcampContext context)
        {
            _context = context;
        }

        public Type Id { get; private set; }

        public async Task<CreditCardDTO> Add(CreateCreditCardModel model)
        {
            var query = _context.CreditCards
                    .Include(c => c.Currency)
                    .AsQueryable();


            var creditCardToCreate = model.Adapt<CreditCard>();

            var creditCardCurrency = await _context.Currencies.FindAsync(creditCardToCreate.CurrencyId);

            var creditCardCustomer = await _context.Customers
           .Include(c => c.Bank)
           .FirstOrDefaultAsync(c => c.Id == model.CustomerId);


            _context.CreditCards.Add(creditCardToCreate);

            await _context.SaveChangesAsync();

            var creditCardDTO = creditCardToCreate.Adapt<CreditCardDTO>();

            return creditCardDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var creditcard = await _context.FindAsync(Id);

            if (creditcard is null) throw new Exception("Creditcard not found");

            _context.Remove(Id);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public Task<CreditCardDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter)
        {
            throw new NotImplementedException();
        }

        public Task<CreditCardDTO> Update(UpdateCreditCardModel model)
        {
            throw new NotImplementedException();
        }
    }
}

