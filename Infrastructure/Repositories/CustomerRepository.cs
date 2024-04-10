﻿using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BootcampContext _context;

    public CustomerRepository(BootcampContext context)
    {
        _context = context;
    }
    public async Task<CustomerDTO> Add(CreateCustomerModel model)
    {
        var bank = await _context.Banks.FindAsync(model.BankId);

        var customerToCreate = model.Adapt<Customer>();

        _context.Add(customerToCreate);
        await _context.SaveChangesAsync();

        var customerDTO = customerToCreate.Adapt<CustomerDTO>();

        return customerDTO;
    }


    public async Task<List<CustomerDTO>> GetFiltered(FilterCustomerModel filter)
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
            query = query.Where(x =>
                (x.Name + " " + x.Lastname).Equals(filter.FullName));
        }


        if (filter.DocumentNumber is not null)
        {
            query = query.Where(x =>
                 x.DocumentNumber != null &&
                (x.DocumentNumber).Equals(filter.DocumentNumber));
        }

        if (filter.Mail is not null)
        {
            query = query.Where(x =>
                 x.Mail != null &&
                (x.Mail).Equals(filter.Mail));
        }

        if (filter.BankId is not null)
        {
            query = query.Where(x =>
                (x.BankId).Equals(filter.BankId));
        }

        var result = await query.ToListAsync();
        var customerDTO = result.Adapt<List<CustomerDTO>>();

        //return result.Select(x => new CustomerDTO
        //{
        //    Id = x.Id,
        //    Name = x.Name,
        //    Lastname = x.Lastname,
        //    DocumentNumber = x.DocumentNumber,
        //    Address = x.Address,
        //    Mail = x.Mail,
        //    Phone = x.Phone,
        //    CustomerStatus = nameof(x.CustomerStatus),
        //    Birth = x.Birth,
        //    Bank = new BankDTO
        //    {
        //        Id = x.Bank.Id,
        //        Name = x.Bank.Name,
        //        Phone = x.Bank.Phone,
        //        Mail = x.Bank.Mail,
        //        Address = x.Bank.Address
        //    }
        //}).ToList();
        return customerDTO;
    }

    public async Task<bool> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer is null) throw new Exception("Customer not found");

        _context.Customers.Remove(customer);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<CustomerDTO>> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();
        var banks = await _context.Banks.ToListAsync();

        var customersDTO = customers.Adapt<List<CustomerDTO>>();

        return customersDTO;
    }

    public async Task<CustomerDTO> GetById(int id)
    {

        var banks = await _context.Banks.ToListAsync();
        var customer = await _context.Customers.FindAsync(id);

        if (customer is null) throw new Exception("Customer not found");

        var customerDTO = customer.Adapt<CustomerDTO>();

        return customerDTO;
    }

    public async Task<bool> NameIsAlreadyTaken(string name)
    {
        return await _context.Customers.AnyAsync(customer => customer.Name == name);
    }


    public async Task<CustomerDTO> Update(UpdateCustomerModel model)
    {
        var customer = await _context.Customers.FindAsync(model.Id);
        var bank = await _context.Banks.FindAsync(model.BankId);

        if (customer is null) throw new Exception("Customer was not found");

        model.Adapt(customer);

        _context.Customers.Update(customer);

        await _context.SaveChangesAsync();

        var customerDTO = customer.Adapt<CustomerDTO>();

        return customerDTO;
    }
}