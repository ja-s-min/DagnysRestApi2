using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Data;
using dagnys_api.Entities;
using dagnys_api.ViewModels;

namespace dagnys_api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(DataContext context) : ControllerBase
{
  private readonly DataContext _context = context;

  [HttpGet()]
  public async Task<ActionResult> ListAllProducts()
  {
    var products = await _context.Products
      .Select(product => new
      {
        product.Id,
        product.ProductName,
        product.Price,
        product.PackSize,
        product.BestBefore,
        product.ManufactureDate
      }
      )
      .ToListAsync();
    return Ok(new { success = true, products });
  }

  [HttpGet("{id}")]
  public async Task<ActionResult> FindProduct(int id)
  {
    var product = await _context.Products
      .Select(product => new
      {
        product.Id,
        product.ProductName,
        product.Price,
        product.PackSize,
        product.BestBefore,
        product.ManufactureDate
      }
      )
      .SingleOrDefaultAsync(p => p.Id == id);

    if (product != null)
      return Ok(new { success = true, product });
    else
      return NotFound(new { success = false, message = $"Kunde inte hitta {id}" });
  }

  [HttpPost()]
  public async Task<ActionResult> AddProduct(ProductPostViewModel model)
  {
    
    var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

    if (prod != null)
    {
      return BadRequest(new { success = false, message = $"Produkten finns redan {0}", model.ProductName });
    }

    
    var product = new Product
    {
      Id = model.Id,
      ProductName = model.ProductName,
      Price = model.Price,
      PackSize = model.PackSize
    };

    try
    {
      await _context.Products.AddAsync(product);
      await _context.SaveChangesAsync();

     
      return CreatedAtAction(nameof(FindProduct), new { id = product.Id }, product);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  
  [HttpPatch("{id}")]
  
  public async Task<ActionResult> UpdateProductPrice(int id, [FromQuery] decimal price)
  {
    var prod = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    if (prod == null)
    {
      return NotFound(new { success = false, message = $"Den existerar ej längre {0}", id });
    }

    prod.Price = price;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }

    return NoContent();
  }
}
