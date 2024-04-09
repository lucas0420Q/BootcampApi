using Core.Entities;
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<CurrencyDTO> GetById(int id);
        Task<Currency> Add(CreateCurrencyModel model);
        Task<bool> NameIsAlreadyTaken(string? name);
    }
}
