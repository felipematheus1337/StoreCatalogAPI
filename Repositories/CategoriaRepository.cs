using Microsoft.EntityFrameworkCore;
using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;

namespace StoreCatalogAPI.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters)
    {
        var categorias = await GetAllAsync();

        var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();

        return PagedList<Categoria>.ToPagedList(categoriasOrdenadas, categoriasParameters.PageNumber,
            categoriasParameters.PageSize);

    }
}
