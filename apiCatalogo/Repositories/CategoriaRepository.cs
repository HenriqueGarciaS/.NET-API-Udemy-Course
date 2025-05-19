using apiCatalogo.context;
using apiCatalogo.models;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context){}
    
}