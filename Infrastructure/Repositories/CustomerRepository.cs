using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BootcampContext _context;
        private Customer CustomeToCreate;

        public CustomerRepository(BootcampContext context)
        {
            _context = context;
        }
        public async Task<CustomerDTO> Add(CreateCustomerModel model)
        {
            var query = _context.Customers
                .Include(c => c.Bank)
                .AsQueryable();

            var CustomerToCreate = model.Adapt<CustomerDTO>();


            _context.Customers.Add(CustomeToCreate);

            await _context.SaveChangesAsync();

            object customerToCreate = null;
            var customerBank = await _context.Banks.FindAsync(customerToCreate);

            var customerDTO = CustomerToCreate.Adapt<CustomerDTO>();

            return customerDTO;
        }
        public async Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter)
        {
            var query = _context.Customers
                .Include(c => c.Bank)
                .AsQueryable();

            if (filter.BirthYearFrom is not null)
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
            if (filter.BankId.HasValue)
            {
                query = query.Where(x => x.BankId == filter.BankId.Value);
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
        public async Task<CustomerDTO> Update(int Id, UpdateCustomerModel model)
        {
            var customerToUpdate = await _context.Customers.FindAsync(Id);

            if (customerToUpdate == null)
            {
                throw new ArgumentException("El cliente no existe");
            }

            customerToUpdate.Name = model.Name;
            customerToUpdate.Lastname = model.Lastname;
            customerToUpdate.DocumentNumber = model.DocumentNumber;
            customerToUpdate.Address = model.Address;
            customerToUpdate.Mail = model.Mail;
            customerToUpdate.Phone = model.Phone;
            customerToUpdate.CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus);
            customerToUpdate.Birth = model.Birth;


            if (model.BankId != null)
            {
                var bank = await _context.Banks.FindAsync(model.BankId);
                if (bank == null) throw new Exception("bank was not found");
                customerToUpdate.BankId = bank.Id;
            }

            await _context.SaveChangesAsync();


            var customerBank = await _context.Banks.FindAsync(customerToUpdate.BankId);


            var updatedCustomerDTO = new CustomerDTO
            {
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Lastname = customerToUpdate.Lastname,
                DocumentNumber = customerToUpdate.DocumentNumber,
                Address = customerToUpdate.Address,
                Mail = customerToUpdate.Mail,
                Phone = customerToUpdate.Phone,
                CustomerStatus = nameof(customerToUpdate.CustomerStatus),
                Birth = customerToUpdate.Birth,
                Bank = customerBank != null ? new BankDTO
                {
                    Id = customerBank.Id,
                    Name = customerBank.Name,
                    Phone = customerBank.Phone,
                    Mail = customerBank.Mail,
                    Address = customerBank.Address
                } : null
            };

            return updatedCustomerDTO;
        }
        public async Task<bool> Delete(int id)
        {
            var Customers = await _context.Customers.FindAsync(id);

            if (Customers is null) throw new Exception("Customer not found");

            _context.Customers.Remove(Customers);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public Task<CustomerDTO> Add(CreateCustomerModel model, object customerToCreate)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerDTO>> GetAll(FilterCustomersModel filter)
        {
            throw new NotImplementedException();
        }
    }
}