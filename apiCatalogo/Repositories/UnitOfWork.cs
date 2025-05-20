using apiCatalogo.context;

namespace apiCatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository ProdutoRepo;
    private ICategoriaRepository CategoriaRepo;

    public AppDbContext Context;

    public UnitOfWork(AppDbContext context)
    {
        Context = context;
    }
    
    public IProdutoRepository ProdutoRepository
    {
        get { return ProdutoRepo = ProdutoRepo ?? new ProdutoRepository(Context); }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get { return CategoriaRepo = CategoriaRepo ?? new CategoriaRepository(Context); }
    }

    public void Commit()
    {
       Context.SaveChanges(); 
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}