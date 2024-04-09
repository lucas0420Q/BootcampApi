using Core.Entities;
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyDTO> GetById(int id);
        Task<CurrencyDTO> Add(CreateCurrencyModel model);
        Task<CustomerDTO> Update(UpdateCustomerModel model);
        Task<bool> Delete(int id);
        Task<List<CustomerDTO>> GetAll();
    }
}
