using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;

namespace StoreCatalogAPI.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);
    
}
