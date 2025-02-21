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
                        return StatusCode(500, "Internal server error");
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
    }
}

 /*[HttpPut("{id}/contact-person")]
        public async Task<IActionResult> UpdateCustomerContactPerson(int id, [FromBody] string newContactPerson)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.Find(id);
                if (customer == null)
                {
                    return NotFound(new { success = false, message = $"Kund med id {id} hittades inte." });
                }

                customer.ContactPerson = newContactPerson;
                await _unitOfWork.Complete();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Fel vid uppdatering av kontaktperson: {ex.Message}" });
            }
        }

       
        [HttpGet("purchased-products/{customerId}")]
        public async Task<IActionResult> GetCustomerProducts(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.Find(customerId);

                if (customer == null)
                {
                    return NotFound(new { success = false, message = $"Kund med id {customerId} hittades inte." });
                }

                var purchasedProducts = await _unitOfWork.CustomerRepository.GetPurchasedProducts(customerId);

                return Ok(new { success = true, data = purchasedProducts });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Fel vid hämtning av produkter: {ex.Message}" });
            }
        }

       
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetOrderWithDetails(orderId);
                if (order == null)
                {
                    return NotFound(new { success = false, message = $"Order med id {orderId} hittades inte." });
                }

                return Ok(new { success = true, data = order });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Fel vid hämtning av beställning: {ex.Message}" });
            }
        }*/
