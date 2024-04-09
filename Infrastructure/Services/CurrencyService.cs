using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;

namespace Infrastructure.Services
{

    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyDTO> Add(CreateCurrencyModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> Update(UpdateCustomerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
