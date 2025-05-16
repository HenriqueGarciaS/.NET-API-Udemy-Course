using apiCatalogo.models;

namespace apiCatalogo.Repositories;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();
    Categoria GetCategoriaById(int id);
    IEnumerable<Categoria> GetCategoriasProdutos();
    Categoria CreateCategoria(Categoria categoria);
    Categoria UpdateCategoria(Categoria categoria);
    Categoria DeleteCategoria(int id);
}