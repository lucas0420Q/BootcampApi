﻿using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IEnterpriseRepository
{
    Task<EnterpriseDTO> GetById(int id);
    Task<EnterpriseDTO> Add(CreateEnterpriseModel model);
    Task<EnterpriseDTO> Update(UpdateEnterpriseModel model);
    Task<bool> Delete(int id);
    Task<List<EnterpriseDTO>> GetAll();
}