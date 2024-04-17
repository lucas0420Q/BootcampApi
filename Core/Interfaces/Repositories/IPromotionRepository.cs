using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface IPromotionRepository
{
    Task<PromotionDTO> GetById(int id);
    Task<PromotionDTO> Add(CreatePromotionModel model);
    Task<PromotionDTO> Update(UpdatePromotionModel model);
    Task<bool> Delete(int id);
    Task<List<PromotionDTO>> GetAll();
}