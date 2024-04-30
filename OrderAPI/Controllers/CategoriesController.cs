using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Context;
using OrderAPI.Models;

namespace OrderAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _context.Categories.AsNoTracking().ToList();
        if (categories is null)
        {
            return NotFound("No categories was founded.");
        }
        return categories;
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
        if (category == null)
        {
            return NotFound($"Product with id {id} not found");
        }
        return Ok(category);
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        if (category == null)
        {
            return BadRequest("Category Param is null");
        }

        _context.Categories.Add(category);
        _context.SaveChanges();

        return new CreatedAtRouteResult("GetCategory",
            new { id = category.CategoryId }, category);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest($"CategoryId {id} is not equal to id in Category");
        }

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category == null)
        {
            return NotFound($"Product with id {id} not found");
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();
        return Ok();
    }
}
