﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Context;
using OrderAPI.Models;

namespace OrderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        var products = await _context.Products.Include(p => p.Category).AsNoTracking().ToListAsync();
        if (products is null)
        {
            return NotFound("No products is available");
        }
        return products;
    }

    [HttpGet("category/{id:int:min(1)}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductbyCategory(int id)
    {
        var products = await _context.Products.Include(p => p.Category).Where(p => p.CategoryId == id).AsNoTracking().ToListAsync();
        if (products is null)
        {
            return NotFound($"No products with category id = {id} is available");
        }
        return products;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        if(products is null)
        {
            return NotFound("No products is available");
        }
        return products;
    }

    [HttpGet("{id:int:min(1)}", Name ="getProduct")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        if(product == null)
        {
            return NotFound($"Product with id {id} not found");
        }
        return product;
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        if(product == null)
        {
            return BadRequest("Product parameter is null");
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetProduct", 
            new { id = product.ProductId }, product);
    }

    [HttpPut("products/{id:int:min(1)}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest($"ProductId {id} is not equal to id in Product");
        }

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(product);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id) 
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

        if (product is null)
        {
            return NotFound($"Product with id {id} not found");
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        return Ok(product);
    }
}
