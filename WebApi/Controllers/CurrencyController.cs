using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CurrencyController : BaseApiController
    {
        private readonly ICurrencyService _service;

        public CurrencyController(ICurrencyService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var currency = await _service.GetById(id);
            return Ok(currency);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyModel request)
        {
            return Ok(await _currencyService.Add(request));
        }

    }
}
