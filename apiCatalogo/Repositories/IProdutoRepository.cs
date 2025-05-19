using apiCatalogo.models;

namespace apiCatalogo.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria);
}