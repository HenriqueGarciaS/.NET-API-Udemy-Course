using apiCatalogo.context;
using apiCatalogo.models;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> GetProdutos()
    {
        return _context.Produtos.ToList();
    }

    public Produto GetProdutoById(int id)
    {
        return _context.Produtos.FirstOrDefault(p => p.Id == id);
    }

    public Produto CreateProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }

    public Produto UpdateProduto(Produto produto)
    {
        if(produto == null)
            throw new ArgumentNullException(nameof(produto));
        
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return produto;
    }

    public Produto DeleteProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        
        if(produto == null)
            throw new ArgumentNullException(nameof(produto));
        
        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return produto;
    }
}