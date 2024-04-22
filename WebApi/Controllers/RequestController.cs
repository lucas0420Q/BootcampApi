using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
public class RequestController : BaseApiController
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestModel request)
    {
        return Ok(await _requestService.Add(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var request = await _requestService.GetById(id);
        return Ok(request);
    }
}




