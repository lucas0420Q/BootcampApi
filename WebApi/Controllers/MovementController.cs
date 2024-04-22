using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class MovementController : BaseApiController
{
    private readonly IMovementService _service;

    public MovementController(IMovementService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMovementModel model)
    {
        return Ok(await _service.Add(model));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var model = await _service.GetById(id);
        return Ok(model);
    }
}
