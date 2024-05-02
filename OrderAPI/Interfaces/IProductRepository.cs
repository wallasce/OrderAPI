using OrderAPI.Models;

namespace OrderAPI.Interfaces;

public interface IProductRepository : ICRUDRepository<Product>
{
    IEnumerable<Product> GetProductById(int id);
}
