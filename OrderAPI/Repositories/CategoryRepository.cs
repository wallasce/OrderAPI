using OrderAPI.Context;
using OrderAPI.Interfaces;
using OrderAPI.Models;

namespace OrderAPI.Repositories;

public class CategoryRepository : CRUDRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
        
    }
}
