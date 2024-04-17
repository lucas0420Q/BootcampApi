using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class RequestController : BaseApiController
    {
        private readonly IRequestService _service;

        public RequestController(IRequestService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Aquí puedes mapear los datos de requestModel a tu entidad Request si es necesario
            var request = new Request
            {
                Name = requestModel.Name,
                RequestDate = requestModel.RequestDate,
                BankApprovalDate = requestModel.BankApprovalDate,
                ProductId = requestModel.ProductId,
                Product = requestModel.Product
            };

            var createdRequest = await _service.CreateRequest(request);
            return CreatedAtAction(nameof(GetRequest), new { id = createdRequest.Id }, createdRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var request = await _service.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }
    }
}
   
        
            

