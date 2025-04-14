using Microsoft.EntityFrameworkCore;
using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;

namespace StoreCatalogAPI.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}
