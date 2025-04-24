using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;

namespace StoreCatalogAPI.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
}
