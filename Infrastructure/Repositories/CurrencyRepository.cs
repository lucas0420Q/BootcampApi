﻿using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly BootcampContext _context;

    public CurrencyRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<CurrencyDTO> Add(CreateCurrencyModel model)
    {

        var currencyToCreate = model.Adapt<Currency>();

        _context.Currencies.Add(currencyToCreate);

        await _context.SaveChangesAsync();

        var currencyDTO = currencyToCreate.Adapt<CurrencyDTO>();

        return currencyDTO;
    }

    public async Task<bool> Delete(int id)
    {

        var currency = await _context.Currencies.FindAsync(id);

        if (currency is null) throw new Exception("Bank not found");

        _context.Currencies.Remove(currency);

        var result = await _context.SaveChangesAsync();

        return result > 0;

    }

    public async Task<CurrencyDTO> GetById(int id)
    {


        var currency = await _context.Currencies.FindAsync(id);

        if (currency is null) throw new NotFoundException($"Currency with id: {id} doest not exist");

        var currencyDTO = currency.Adapt<CurrencyDTO>();

        return currencyDTO;
    }

    public async Task<List<CurrencyDTO>> GetFiltered(FilterCurrencyModel filter)
    {
        var query = _context.Currencies.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            string filterNameLower = filter.Name.ToLower();
            query = query.Where(x => x.Name.ToLower() == filterNameLower);
        }
        var result = await query.ToListAsync();

        var currencyDTOs = result.Adapt<List<CurrencyDTO>>();

        return currencyDTOs;
    }
    public async Task<CurrencyDTO> Update(UpdateCurrencyModel model)
    {
        var currency = await _context.Currencies.FindAsync(model.Id);

        if (currency is null) throw new Exception("Currency was not found");

        model.Adapt(currency);

        _context.Currencies.Update(currency);

        await _context.SaveChangesAsync();

        var currencyDTO = currency.Adapt<CurrencyDTO>();

        return currencyDTO;
    }

}

