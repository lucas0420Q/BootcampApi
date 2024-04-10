﻿using Core.Models;
using Core.Request;


namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomerModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model);
    Task<CustomerDTO> GetById(int id);
    Task<CustomerDTO> Update(UpdateCustomerModel model);
    Task<bool> Delete(int id);
    Task<List<CustomerDTO>> GetAll();
    Task<bool> NameIsAlreadyTaken(string name);
}