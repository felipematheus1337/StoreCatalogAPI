using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StoreCatalogAPI.Context;

namespace StoreCatalogAPI.Repositories;

public class Repository<T> : IRepository<T> where T : class
{

    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);

        return entity;
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);

    }

    public IEnumerable<T> GetAll()
    {
       return _context.Set<T>().AsNoTracking().ToList();
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }
}
