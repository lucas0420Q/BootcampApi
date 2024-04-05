using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Requests;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly BootcampContext _context;

    public BankRepository(BootcampContext context)
    {
        _context = context;
    }

    public async Task<BankDTO> Add(CreateBankModel model)
    {
        var banktocreate = new Bank
        {
            Name = model.Name,
            Address = model.Address,
            Mail = model.Mail,
            Phone = model.Phone,
        };

        _context.Banks.Add(banktocreate);

        await _context.SaveChangesAsync();

        var bankdto = new BankDTO
        {
            Id = banktocreate.Id,
            Name = banktocreate.Name,
            Address = banktocreate.Address,
            Mail = banktocreate.Mail,
            Phone = banktocreate.Phone
        };

        return bankdto;
    }

    public async Task<bool> Delete(int id)
    {
        var bank = await _context.Banks.FindAsync(id);

        if (bank is null) throw new Exception("bank not found");

        _context.Banks.Remove(bank);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<BankDTO>> GetAll()
    {
        var banks = await _context.Banks.ToListAsync();

        var banksdto = banks.Select(bank => new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        }).ToList();

        return banksdto;
    }

    public async Task<BankDTO> GetById(int id)
    {
        var bank = await _context.Banks.FindAsync(id);

        if (bank is null) throw new Exception("bank not found");

        var bankdto = new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        };

        return bankdto;
    }

    public async Task<bool> NameIsAlreadyTaken(string name)
    {
        return await _context.Banks.AnyAsync(bank => bank.Name == name);
    }

    public async Task<BankDTO> Update(UpdateBankModel model)
    {
        var bank = await _context.Banks.FindAsync(model.Id);

        if (bank is null) throw new Exception("bank was not found");

        bank.Name = model.Name;
        bank.Address = model.Address;
        bank.Mail = model.Mail;
        bank.Phone = model.Phone;

        _context.Banks.Update(bank);

        await _context.SaveChangesAsync();

        var bankdto = new BankDTO
        {
            Id = bank.Id,
            Name = bank.Name,
            Address = bank.Address,
            Mail = bank.Mail,
            Phone = bank.Phone
        };

        return bankdto;
    }
}
