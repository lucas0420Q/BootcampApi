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

    public async Task<RequestDTO> Add(CreateRequestModel request)
    {

        var product = request.Adapt<Request>();

        _context.Requests.Add(product);

        await _context.SaveChangesAsync();

        var createdProduct = await _context.Requests
        .Include(pr => pr.Currency)
        .Include(pr => pr.Customer)
            .ThenInclude(c => c.Bank)
        .FirstOrDefaultAsync(pr => pr.Id == product.Id);


        var RequestDTO = createdProduct.Adapt<RequestDTO>();

        return RequestDTO;
    }
}
//public async Task<RequestDTO> GetById(int id)
//    {
//        var request = await _context.Requests
//           .Include(r => r.Currency)
//           .Include(r => r.Status)
//           .Include(r => r.Customer)
//           .ThenInclude(r => r.Bank)
//           .SingleOrDefaultAsync(r => r.Id == id);
//        if (request != null)
//        {
//            return request.Adapt<RequestDTO>();
//        }
//        else
//        {
//            return null;
//        }
//    }


