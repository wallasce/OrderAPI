using OrderAPI.Models;

namespace OrderAPI.Interfaces;

public interface IProductRepository : ICRUDRepository<Product>
{
    IEnumerable<Product> GetProductByCategoryId(int id);
}
