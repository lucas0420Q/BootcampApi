using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _businessRepository;
    public EnterpriseService(IEnterpriseRepository businessRepository)
    {
        _businessRepository = businessRepository;
    }

    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {
        return await _businessRepository.Add(model);
    }

    public async Task<bool> Delete(int id)
    {
        return await _businessRepository.Delete(id);
    }

    public async Task<List<EnterpriseDTO>> GetAll()
    {
        return await _businessRepository.GetAll();
    }

    public async Task<EnterpriseDTO> GetById(int id)
    {
        return await _businessRepository.GetById(id);
    }

    public async Task<EnterpriseDTO> Update(UpdateEnterpriseModel model)
    {
        return await _businessRepository.Update(model);
    }
}
