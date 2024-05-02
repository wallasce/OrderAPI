namespace OrderAPI.Repositories;

public interface IRepositoryCRUDBase<T>
{
    IEnumerable<T> GetAll();
    T Get(int id);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
