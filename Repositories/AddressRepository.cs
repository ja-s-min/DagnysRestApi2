using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Entities;
using dagnys_api.ViewModels.Address;

namespace dagnys_api.Repositories
{
    public class AddressRepository : IAddressRepository
    {
    private readonly DataContext _context;

    public AddressRepository(DataContext context)
        {
            _context = context;
        }

    public async Task<Address> Add(AddressPostViewModel model)
        {
            try
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
                    && c.AddressTypeId == (int)model.AddressType);

                if (address == null)
                {
                  
                    address = new Address
                    {
                        AddressLine = model.AddressLine.Trim(),
                        AddressTypeId = (int)model.AddressType,  
                        PostalAddress = postalAddress
                    };

                    await _context.Addresses.AddAsync(address);
                }

               
                await _context.SaveChangesAsync();
                return address; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel inträffade när adressen skulle läggas till: {ex.Message}");
            }
        }

        
public async Task<bool> Add(string type)
        {
            try
            {
                var exists = await _context.AddressTypes
                    .FirstOrDefaultAsync(c => c.Value.ToLower() == type.ToLower());

                if (exists != null) 
                    throw new Exception($"Adresstypen {type} finns redan");

                var newType = new AddressType
                {
                    Value = type
                };

                await _context.AddressTypes.AddAsync(newType);
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Fel vid tillägg av AddressType: {ex.Message}");
            }
        }

     
        public async Task<bool> Remove(int id)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(id);
                if (address == null) return false;

                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return true;  
            }
            catch (Exception ex)
            {
                throw new Exception($"Fel vid borttagning av adress: {ex.Message}");
            }
        }

        
        public async Task<Address> Find(int id)
        {
            try
            {
                var address = await _context.Addresses
                    .Include(a => a.PostalAddress)  
                    .Include(a => a.AddressType)    
                    .SingleOrDefaultAsync(a => a.Id == id);

                if (address == null) 
                {
                    throw new Exception($"Adressen med id {id} hittades inte.");
                }

                return address;
            }
            catch (Exception ex)
            {
                throw new Exception($"Fel vid hämtning av adress: {ex.Message}");
            }
        }
    }
}
