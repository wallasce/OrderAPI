using OrderAPI.Context;
using OrderAPI.Interfaces;
using OrderAPI.Models;

namespace OrderAPI.Repositories;

public class ProductRepository : CRUDRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
        
    }

    public IEnumerable<Product> GetProductById(int id)
    {
        return GetAll().Where(p => p.ProductId == id);
    }
}
