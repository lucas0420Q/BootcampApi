using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : BaseApiController
    {


        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountModel filter)
        {
            return Ok(await _service.Add(filter));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAccountModel filter)
        {
            return Ok(await _service.Update(filter));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}

