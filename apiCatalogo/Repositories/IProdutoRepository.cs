using apiCatalogo.models;

namespace apiCatalogo.Repositories;

public interface IProdutoRepository
{
    public IEnumerable<Produto> GetProdutos();
    public Produto GetProdutoById(int id);
    public Produto CreateProduto(Produto produto);
    public Produto UpdateProduto(Produto produto);
    public Produto DeleteProduto(int id);
}