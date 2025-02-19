using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dagnys_api.Interfaces;
using dagnys_api.ViewModels.Customer;

namespace dagnys_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

    public CustomersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost()]
    public async Task<ActionResult> AddCustomer([FromBody] CustomerPostViewModel model)
    {
        try
        {
            if (await _unitOfWork.CustomerRepository.Add(model))
            {
                if (await _unitOfWork.Complete())
                {
                    return StatusCode(201);
                }
                return StatusCode(500);
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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
            var customer = await _unitOfWork.CustomerRepository.Find(id);
            return Ok(new { success = true, data = customer });
        }
        catch (Exception ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
    }
}