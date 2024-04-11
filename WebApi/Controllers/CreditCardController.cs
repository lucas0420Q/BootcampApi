using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Controllers;

public class CreditCardController : BaseApiController
{
    private readonly ICreditCardService _service;

    public CreditCardController(ICreditCardService creditCardService)
    {
        _service = creditCardService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCreditCardModel request)
    {
        return Ok(await _service.Add(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var creditCard = await _service.GetById(id);
        return Ok(creditCard);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCreditCardModel request)
    {
        return Ok(await _service.Update(request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCreditCardModel filter)
    {
        var creditCard = await _service.GetFiltered(filter);
        return Ok(creditCard);
    }
}
