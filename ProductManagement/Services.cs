using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Entities;

namespace ProductManagement;


[ApiController]
[Route("api/products")]
public class AppServices : ControllerBase
{
    private readonly AppDbContext _context;

    public AppServices(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProductById(int id)
    {
        var product = _context.Products.Include(p => p.Manufacturer).FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

}