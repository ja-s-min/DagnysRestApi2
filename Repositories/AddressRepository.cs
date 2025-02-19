using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using dagnys_api.Repositories;
using dagnys_api.ViewModels.Address;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Entities;
using Microsoft.AspNetCore;

namespace dagnys_api;
public class AddressRepository : IAddressRepository
{
    private readonly DataContext _context;

    public AddressRepository(DataContext context)
    {
        _context = context;
    }

 
   public async Task<Address> Add(AddressPostViewModel model)
{
    var postalAddress = await _context.PostalAddresses
        .FirstOrDefaultAsync(c => c.PostalCode.Replace(" ", "").Trim() == model.PostalCode.Replace(" ", "").Trim());

    
    if (postalAddress == null)
    {
        postalAddress = new PostalAddress
        {
            PostalCode = model.PostalCode.Replace(" ", "").Trim(),
            City = model.City.Trim()
        };
        await _context.PostalAddresses.AddAsync(postalAddress);
    }

    
    var address = await _context.Addresses
        .FirstOrDefaultAsync(c => c.AddressLine.Trim().ToLower() == model.AddressLine.Trim().ToLower()
        && c.AddressTypeId == model.AddressTypeId); 

    if (address == null)
    {
       
        address = new Address
        {
            AddressLine = model.AddressLine,
            AddressTypeId = model.AddressTypeId,  
            PostalAddress = postalAddress
        };

        await _context.Addresses.AddAsync(address);
    }

    await _context.SaveChangesAsync();
    return address;
}

    
    public async Task<bool> Add(string type)
    {
        var exists = await _context.AddressTypes
            .FirstOrDefaultAsync(c => c.Value.ToLower() == type.ToLower());

        if (exists != null) 
            throw new Exception($"Adress typen {type} finns redan");

        var newType = new AddressType
        {
            Value = type
        };

        await _context.AddressTypes.AddAsync(newType);
        await _context.SaveChangesAsync();
        return true;
    }

 
   public async Task<bool> Remove(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return false;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

    
    public async Task<Address> Find(int id)
    {
        return await _context.Addresses
            .Include(a => a.PostalAddress)
            .Include(a => a.AddressType)
            .SingleOrDefaultAsync(a => a.Id == id);
    }
}
    