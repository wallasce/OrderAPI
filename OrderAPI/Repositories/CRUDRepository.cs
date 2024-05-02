using Microsoft.EntityFrameworkCore;
using OrderAPI.Context;
using OrderAPI.Interfaces;
using System.Linq.Expressions;

namespace OrderAPI.Repositories;

public class CRUDRepository<T> : ICRUDRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public CRUDRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}
