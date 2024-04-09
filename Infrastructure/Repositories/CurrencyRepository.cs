using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;

namespace Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly BootcampContext _context;

        public CurrencyRepository(BootcampContext context)
        {
            _context = context;
        }

        public Task<Currency> Add(CreateCurrencyModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<CurrencyDTO> GetById(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);

            //if (bank is null) throw new Exception("Bank not found");

            if (currency is null) throw new NotFoundException($"Bank with id: {id} doest not exist");

            var CurrencyDTO = currency.Adapt<CurrencyDTO>();

            return CurrencyDTO;
        }

        public Task<bool> NameIsAlreadyTaken(string? name)
        {
            throw new NotImplementedException();
        }
    }
}

