using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    namespace BusinessController

    {
        [Route("api/[controller]")]
        [ApiController]
        public class BusinessController : BaseApiController
        {
            private readonly IPromotionRepository _promocionRepository;

            public BusinessController(IPromotionRepository promocionRepository)
            {
                _promocionRepository = promocionRepository;
            }

        }
    }
}

   

