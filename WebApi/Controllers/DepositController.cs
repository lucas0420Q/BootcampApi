using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class DepositController : BaseApiController
{
    private readonly IDepositService _depositservice;

    public DepositController(IDepositService depositservice)
    {
        _depositservice = depositservice;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepositModel model)
    {
        return Ok(await _depositservice.Add(model));
    }
}
