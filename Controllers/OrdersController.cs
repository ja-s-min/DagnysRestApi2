using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dagnys_api.Entities;
using dagnys_api.Interfaces;
using dagnys_api.ViewModels;

namespace dagnys_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
    private readonly IUnitOfWork _unitOfWork;

    public OrdersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost()]
    public async Task<ActionResult> AddOrder([FromBody] OrderViewModel order)
    {
        var newOrder = new Order
        {
            OrderDate = order.OrderDate,
            CustomerId = order.CustomerId,
            OrderItems = order.Products.Select(p => new OrderItem
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                Price = p.Price
            }).ToList()
        };

        await _unitOfWork.OrderRepository.Add(newOrder);
        await _unitOfWork.Complete();
        return CreatedAtAction(nameof(GetOrder), new { id = newOrder.OrderId }, newOrder);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrder(int id)
    {
        var order = await _unitOfWork.OrderRepository.Find(id);
        if (order == null)
            return NotFound();

        return Ok(new { success = true, data = order });
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllOrders([FromQuery] DateTime? orderDate)
    {
        var orders = await _unitOfWork.OrderRepository.List(orderDate);
        return Ok(new { success = true, data = orders });
    }

    [HttpGet("search")]
public async Task<ActionResult> SearchOrders([FromQuery] int? orderId, [FromQuery] DateTime? orderDate)
{
    try
    {
        var orders = await _unitOfWork.OrderRepository.Search(orderId, orderDate);
        return Ok(new { success = true, data = orders });
    }
    catch (Exception ex)
    {
        return BadRequest(new { success = false, message = ex.Message });
    }
}
    }
}