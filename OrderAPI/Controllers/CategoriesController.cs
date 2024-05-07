using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.DTOs;
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
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<CategoryDTO>> Get()
    {
        var categories = _unitOfWork.CategoryRepository.GetAll();
        if (categories is null)
        {
            return NotFound("No categories was founded.");
        }

        var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        return Ok(categoriesDto);
    }

    [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
    public ActionResult<CategoryDTO> Get(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == id);
        if (category == null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var categoryDto = _mapper.Map<CategoryDTO>(category);
        return Ok(categoryDto);
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Post(CategoryDTO categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest("Category Param is null");
        }

        var category = _mapper.Map<Category>(categoryDto);

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

        var categoryCreatedDto = _mapper.Map<CategoryDTO>(categoryCreated);
        return new CreatedAtRouteResult("GetCategory",
            new { id = categoryCreatedDto.CategoryId }, categoryCreatedDto);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDto)
    {
        if (id != categoryDto.CategoryId)
        {
            return BadRequest($"CategoryId {id} is not equal to id in Category");
        }

        var category = _mapper.Map<Category>(categoryDto);
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

        var categoryChangedDto = _mapper.Map<CategoryDTO>(categoryChaged);
        return Ok(categoryChangedDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<CategoryDTO> Delete(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(c =>  c.CategoryId == id);

        if (category == null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var categoryDeleted = _unitOfWork.CategoryRepository.Delete(category);
        _unitOfWork.Commit();

        var categoryDeletedDto = _mapper.Map<CategoryDTO>(categoryDeleted);
        return Ok(categoryDeletedDto);
    }
}
