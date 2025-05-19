using apiCatalogo.context;

namespace apiCatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public T Create(T model)
    {
        _context.Set<T>().Add(model);
        _context.SaveChanges();
        return model;
    }

    public T Update(T model)
    {
        _context.Set<T>().Update(model);
        _context.SaveChanges();
        return model;
    }

    public T Delete(int id)
    {
        var model = _context.Set<T>().Find(id);
        _context.Set<T>().Remove(model);
        _context.SaveChanges();
        return model;
    }
}