using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class ExtractionController : BaseApiController
{
    private readonly IExtractionService _depositservice;

    public ExtractionController(IExtractionService depositservice)
    {
        _depositservice = depositservice;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExtractionModel model)
    {
        return Ok(await _depositservice.Add(model));
    }
}
