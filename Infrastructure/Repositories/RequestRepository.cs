using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly BootcampContext _context;
    public RequestRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<RequestDTO> Add(CreateRequestModel model)
    {
        var request = model.Adapt<Request>();
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        var createRequest = await _context.Requests
        .Include(r => r.Currency)
        .Include(r => r.Product)
        .Include(r => r.Customer)
        .ThenInclude(r => r.Bank)
        .SingleOrDefaultAsync(r => r.Id == request.Id);
        return createRequest.Adapt<RequestDTO>(); ;
    }

    public async Task<RequestDTO> GetById(int id)
    {
        var request = await _context.Requests
           .Include(r => r.Currency)
           .Include(r => r.Product)
           .Include(r => r.Customer)
           .ThenInclude(r => r.Bank)
           .SingleOrDefaultAsync(r => r.Id == id);
        if (request != null)
        {
            return request.Adapt<RequestDTO>();
        }
        else
        {
            return null;
        }
    }
}



