using Microsoft.AspNetCore.Mvc;
using OrderAPI.Interfaces;
using OrderAPI.Models;
using OrderAPI.Validations;

namespace OrderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("category/{id:int:min(1)}")]
    public ActionResult<IEnumerable<Product>> GetProductbyCategory(int id)
    {
        var products = _unitOfWork.ProductRepository.GetProductByCategoryId(id);
        if (products is null)
        {
            return NotFound($"No products with category id = {id} is available");
        }
        return Ok(products);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _unitOfWork.ProductRepository.GetAll();
        if(products is null)
        {
            return NotFound("No products is available");
        }
        return Ok(products);
    }

    [HttpGet("{id:int:min(1)}", Name ="getProduct")]
    public ActionResult<Product> Get(int id)
    {
        var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);
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

        return new CreatedAtRouteResult("GetProduct", 
            new { id = product.ProductId }, productCreated);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest($"ProductId {id} is not equal to id in Product");
        }

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

        return Ok(productChanged);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id) 
    {
        var product = _unitOfWork.ProductRepository.Get(p => p.ProductId == id);

        if (product is null)
        {
            return NotFound($"Product with id {id} not found");
        }

        var productDeleted = _unitOfWork.ProductRepository.Delete(product);
        _unitOfWork.Commit();

        return Ok(productDeleted);
    }
}
