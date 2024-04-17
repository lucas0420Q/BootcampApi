using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly BootcampContext _context;
    public EnterpriseRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<EnterpriseDTO> Add(CreateEnterpriseModel model)
    {
        var businessToCreate = model.Adapt<Enterprise>();
        _context.Enterprises.Add(businessToCreate);

        await _context.SaveChangesAsync();
        var businessDTO = businessToCreate.Adapt<EnterpriseDTO>();
        return businessDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var business = await _context.Enterprises.FindAsync(id);

        if (business is null) throw new Exception("Business not found");

        _context.Enterprises.Remove(business);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<EnterpriseDTO>> GetAll()
    {
        var enterprises = await _context.Enterprises
        .Include(e => e.PromotionsEnterprises)
            .ThenInclude(pe => pe.Promotion)
        .Select(e => new EnterpriseDTO
        {
            Id = e.Id,
            Name = e.Name,
            Address = e.Address,
            Phone = e.Phone,
            Email = e.Email,
            Promotions = e.PromotionsEnterprises.Select(pe => pe.Promotion.Adapt<PromotionDTO>()).ToList()
        })
        .ToListAsync();

        return enterprises;
    }
    public async Task<EnterpriseDTO> GetById(int id)
    {
        var business = await _context.Enterprises.FindAsync(id);

        if (business is null) throw new NotFoundException($"Business with id: {id} doest not exist");

        var businessDTO = business.Adapt<EnterpriseDTO>();

        return businessDTO;
    }

    public async Task<EnterpriseDTO> Update(UpdateEnterpriseModel model)
    {
        var business = await _context.Enterprises.FindAsync(model.Id);

        if (business is null) throw new Exception("Business was not found");

        model.Adapt(business);

        _context.Enterprises.Update(business);

        await _context.SaveChangesAsync();

        var EnterpriseDTO = business.Adapt<EnterpriseDTO>();

        return EnterpriseDTO;
    }
}
