using Microsoft.AspNetCore.Mvc;
using OrderAPI.Filters;
using OrderAPI.Interfaces;
using OrderAPI.Models;
using OrderAPI.Validations;

namespace OrderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _unitOfWork.CategoryRepository.GetAll();
        if (categories is null)
        {
            return NotFound("No categories was founded.");
        }
        return Ok(categories);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    public ActionResult<Category> Get(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);
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

        var validation = new CategoryValidator()
            .RuleName()
            .Validate(category);

        if (!validation.IsValid)
        {
            string errorStr = "";
            foreach (var error in validation.Errors)
            {
                errorStr += error + "\n";
            }
            return BadRequest(errorStr);
        }

        var categoryCreated = _unitOfWork.CategoryRepository.Create(category);
        _unitOfWork.Commit();

        return new CreatedAtRouteResult("GetCategory",
            new { id = category.CategoryId }, categoryCreated);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest($"CategoryId {id} is not equal to id in Category");
        }

        var validation = new CategoryValidator()
            .RuleName()
            .Validate(category);

        if (!validation.IsValid)
        {
            string errorStr = "";
            foreach (var error in validation.Errors)
            {
                errorStr += error + "\n";
            }
            return BadRequest(errorStr);
        }

        var categoryChaged = _unitOfWork.CategoryRepository.Update(category);
        _unitOfWork.Commit();

        return Ok(categoryChaged);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(c =>  c.CategoryId == id);

        if (category == null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var categoryDeleted = _unitOfWork.CategoryRepository.Delete(category);
        _unitOfWork.Commit();

        return Ok(categoryDeleted);
    }
}
