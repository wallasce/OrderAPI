using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.DTOs;
using OrderAPI.Interfaces;
using OrderAPI.Models;
using OrderAPI.Validations;

namespace OrderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("category/{id:int:min(1)}")]
    public ActionResult<IEnumerable<ProductDTO>> GetProductbyCategory(int id)
    {
        var products = _unitOfWork.ProductRepository.GetProductByCategoryId(id);
        if (products is null)
        {
            return NotFound($"No products with category id = {id} is available");
        }

        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDTO>> Get()
    {
        var products = _unitOfWork.ProductRepository.GetAll();
        if(products is null)
        {
            return NotFound("No products is available");
        }

        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id:int:min(1)}", Name ="getProduct")]
    public ActionResult<ProductDTO> Get(int id)
    {
        var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);
        if(product == null)
        {
            return NotFound($"Product with id {id} not found");
        }
        
        var productDto = _mapper.Map<ProductDTO>(product);
        return productDto;
    }

    [HttpPost]
    public ActionResult<ProductDTO> Post(ProductDTO productDto)
    {
        if (productDto == null)
            return BadRequest("Product parameter is null");
        
        var product = _mapper.Map<Product>(productDto);
        
        var validation = new ProductValidator()
            .RuleCategoryId()
            .RuleName()
            .RuleDescription()
            .RulePrice()
            .RuleServes()
            .Validate(product);

        if(!validation.IsValid)
        {
            string errorStr = "";
            foreach (var error in validation.Errors)
            {
                errorStr += error + "\n";
            }
            return BadRequest(errorStr);
        }

        var productCreated = _unitOfWork.ProductRepository.Create(product);
        _unitOfWork.Commit();

        var newProductDto = _mapper.Map<ProductDTO>(productCreated);
        return new CreatedAtRouteResult("GetProduct", 
            new { id = newProductDto.ProductId }, newProductDto);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
    {
        if (id != productDto.ProductId)
        {
            return BadRequest($"ProductId {id} is not equal to id in Product");
        }

        var product = _mapper.Map<Product>(productDto);

        var validation = new ProductValidator()
            .RuleCategoryId()
            .RuleName()
            .RuleDescription()
            .RulePrice()
            .RuleServes()
            .Validate(product);

        if (!validation.IsValid)
        {
            string errorStr = "";
            foreach (var error in validation.Errors)
            {
                errorStr += error + "\n";
            }
            return BadRequest(errorStr);
        }

        var productChanged = _unitOfWork.ProductRepository.Update(product);
        _unitOfWork.Commit();

        var productChangedDto = _mapper.Map<ProductDTO>(productChanged);
        return Ok(productChangedDto);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<ProductDTO> Delete(int id) 
    {
        var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);

        if (product is null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var productDeleted = _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.Commit();

        var productDeletedDto = _mapper.Map<ProductDTO>(productDeleted);
        return Ok(productDeletedDto);
    }
}
