
using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IEnterpriseService
{
    Task<EnterpriseDTO> Add(CreateEnterpriseModel model);
    Task<EnterpriseDTO> GetById(int id);
    Task<EnterpriseDTO> Update(UpdateEnterpriseModel model);
    Task<bool> Delete(int id);
    Task<List<EnterpriseDTO>> GetAll();
}
