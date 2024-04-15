using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;

namespace Infrastructure.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        public Task<PromotionDTO> Add(CreatePromotionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
