﻿using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class CustomerController : BaseApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> GetFiltered([FromQuery] FilterCustomersModel filter)
    {
        var customers = await _customerService.GetFiltered(filter);
        return Ok(customers);
    }
    [HttpPost("Crear")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerModel request)
    {
        return Ok(await _customerService.Add(request));
    }
    [HttpPut("Actualizar")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerModel model)
    {

        var updatedCustomer = await _customerService.Update(model.Id, model);
        if (updatedCustomer == null)
        {
            return NotFound();
        }
        return Ok(updatedCustomer);
    }
    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _customerService.Delete(id));
    }
}

