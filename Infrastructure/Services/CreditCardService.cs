using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;

namespace Infrastructure.Services;
public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardRepository? _creditcardRepository;
    //public CreditCardService(ICreditCardRepository creditcardRepository);

    //_creditcardRepository creditcardRepository;

    public Task<List<CreditCardDTO>> GetFiltered(FilterCreditCardModel filter)
    {
        throw new NotImplementedException();
    }

    public Task<CreditCardDTO> Add(CreateCreditCardModel model)
    {
        throw new NotImplementedException();
    }

    public Task<CreditCardDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CreditCardDTO> Update(UpdateCreditCardModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }
}


