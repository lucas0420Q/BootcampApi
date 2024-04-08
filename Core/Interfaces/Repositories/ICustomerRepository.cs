using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model, object customerToCreate);
    Task<CustomerDTO> Update(int Id, UpdateCustomerModel model);
    Task<bool> Delete(int id);
    Task<List<CustomerDTO>> GetAll(FilterCustomersModel filter);

}