using Core.Interfaces.Services;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _accountService.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        => Ok(await _accountService.Create(request));
}

