using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Core.Solicitudes;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BootcampContext _context;
    private object CustomerToCreate;

    public CustomerRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<CustomerDTO> Add(CreateCustomerModel model, Customer? customerToCreate)
    {
        var ustomerToCreate = new Customer
        {
            Name = model.Name,
            Lastname = model.Lastname,
            DocumentNumber = model.DocumentNumber,
            Address = model.Address,
            Mail = model.Mail,
            Phone = model.Phone,
            //CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model, CustomerStatus),
            CustomerStatus = nameof(CustomerToCreate.CustomerStatus),
            BankId = model.BankId,
            Bank = new Bank { },
            Birth = (DateTime?)model.Birth
        };


        _context.Customers.Add(customerToCreate);

        await _context.SaveChangesAsync();

        var customerDTO = new CustomerDTO
        {
            Id = customerToCreate.Id,
            Name = customerToCreate.Name,
            Lastname = customerToCreate.Lastname,
            DocumentNumber = customerToCreate.DocumentNumber,
            Address = customerToCreate.Address,
            Mail = customerToCreate.Mail,
            Phone = customerToCreate.Phone,

            Birth = customerToCreate.Birth,
            Bank = new BankDTO
            {
                // Aquí necesitas asignar los valores correspondientes del objeto Bank,
                // basándote en cómo lo obtuviste anteriormente.
            }
        };

        return customerDTO;
    }

    private CustomerStatus nameof(object customerStatus)
    {
        throw new NotImplementedException();
    }

    public Task<List<CustomerDTO>> Add(CreateBankModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter)







    {
       
      var query = _context.Customers
            .Include(c => c.Bank)
            .AsQueryable();

      if(filter.BirthYearFrom is not null)
        {
            query = query.Where(x => 
                x.Birth != null &&
                x.Birth.Value.Year >= filter.BirthYearFrom);
        }

        if (filter.BirthYearTo is not null)
        {
            query = query.Where(x =>
                x.Birth != null &&
                x.Birth.Value.Year <= filter.BirthYearTo);
        }

        if (filter.FullName is not null)
        {
            query = query.Where(x => (x.Name + " " + x.Lastname).Contains(filter.FullName));
        }

        if (filter.DocumentNumber is not null)
        {
            query = query.Where(x => x.DocumentNumber.Contains(filter.DocumentNumber));
        }

        if (filter.Mail is not null)
        {
            query = query.Where(x => x.Mail.Contains(filter.Mail));
        }

        if (filter.BandId.HasValue)
        {
            query = query.Where(x => x.BankId == filter.BandId.Value);
        }

    var result = await query.ToListAsync();

        return result.Select(x => new CustomerDTO
        {
            Id = x.Id,
            Name = x.Name,
            Lastname = x.Lastname,
            DocumentNumber = x.DocumentNumber,
            Address = x.Address,
            Mail = x.Mail,
            Phone = x.Phone,
            CustomerStatus = nameof(x.CustomerStatus),
            Birth = x.Birth,
            Bank = new BankDTO
            {
                Id = x.Bank.Id,
                Name = x.Bank.Name,
                Phone = x.Bank.Phone,
                Mail = x.Bank.Mail,
                Address = x.Bank.Address
            }
        }).ToList();
    }
}




