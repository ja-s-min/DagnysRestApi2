using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dagnys_api.Interfaces;
using dagnys_api.ViewModels.Address;
using dagnys_api.Entities;
using dagnys_api.ViewModels;

namespace dagnys_api.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class CustomersController(IUnitOfWork unitOfWork) : ControllerBase
    {
private readonly IUnitOfWork _unitOfWork = unitOfWork;

[HttpGet()]
public async Task<ActionResult> GetAllCustomers()
  {
    var customers = await _unitOfWork.CustomerRepository.List();
    return Ok(new { success = true, data = customers });
  }

[HttpGet("{id}")]
public async Task<ActionResult> GetCustomer(int id)
  {
    try
    {
      return Ok(new { success = true, data = await _unitOfWork.CustomerRepository.Find(id) });
    }
    catch (Exception ex)
    {
      return NotFound(new { success = false, message = ex.Message });
    }
  }

[HttpPost]
public async Task<ActionResult> AddCustomer([FromBody] CustomerPostViewModel model)
    {
     try
        {
        var result = await _unitOfWork.CustomerRepository.Add(model);
        if (result)
            {
                if (await _unitOfWork.Complete())
                {
                    return StatusCode(201);
                }
                else
                {
                    return StatusCode(500);
                }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Kunde inte lägga till kund" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

[HttpGet("{customerId}/purchases")]
public async Task<IActionResult> GetCustomerPurchases(int customerId)
{
    var customer = await _unitOfWork.CustomerRepository.Find(customerId);

    if (customer == null) 
        return NotFound(new { success = false, message = "Kund hittades inte." });

    var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerId(customerId);

    var purchasedProducts = orders
        .SelectMany(o => o.OrderItems)
        .Select(op => new 
        {
            op.Product.Id,
            op.Product.ProductName,
            op.Product.Price,
            op.Product.Weight,
            op.Product.PackSize
        })
        .Distinct()
        .ToList();

    return Ok(new { success = true, data = purchasedProducts });
}

[HttpPut("{customerId}/contactperson")]
public async Task<IActionResult> UpdateCustomerContactPerson(int customerId, [FromBody] string newContactPerson)
{
    var customerViewModel = await _unitOfWork.CustomerRepository.Find(customerId);

    if (customerViewModel == null)
        return NotFound(new { success = false, message = "Kund hittades inte." });

    Console.WriteLine($"Uppdaterar kund {customerId}: {customerViewModel.ContactPerson} -> {newContactPerson}");

    
    var customer = new Customer
    {
        Id = customerViewModel.Id,
        StoreName = customerViewModel.StoreName,
        Email = customerViewModel.Email,
        Phone = customerViewModel.Phone,
        ContactPerson = newContactPerson,
    };

    _unitOfWork.CustomerRepository.Attach(customer);
    var result = await _unitOfWork.Complete();

    if (!result)
    {
        Console.WriteLine("Systemet hänger ej med riktigt.");
        return StatusCode(500, new { success = false, message = "Misslyckades med att uppdatera kontaktpersonen." });
    }

    Console.WriteLine("Kontaktperson uppdaterad!");
    return Ok(new { success = true, message = "Kontaktpersonen har uppdaterats.", data = customer });
}





    }
}
