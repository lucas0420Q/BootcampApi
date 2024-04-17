using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PromotionController : BaseApiController
{
    private readonly IPromotionService _service;

    public PromotionController(IPromotionService service)
    {
        _service = service;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
=> Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePromotionModel request)
    {
        return Ok(await _service.Add(request));
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePromotionModel request)
    {
        return Ok(await _service.Update(request));
    }
    //[HttpGet("filtered")]
    //public async Task<IActionResult> GetFiltered([FromQuery] FilterPromotionModel filter)
    //{
    //    var account = await _service.GetFiltered(filter);
    //    return Ok(account);
//}

[HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _service.Delete(id));
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }
}