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

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();

        return PagedList<Categoria>.ToPagedList(categorias, categoriasParameters.PageNumber,
            categoriasParameters.PageSize);

    }
}
