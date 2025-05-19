using apiCatalogo.context;
using apiCatalogo.models;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Repositories;

public class ProdutoRepository :  Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context){}

    public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
    {
        return GetAll().Where(c => c.CategoriaId == idCategoria);
    }
}