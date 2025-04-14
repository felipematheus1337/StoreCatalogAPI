using StoreCatalogAPI.Models;

namespace StoreCatalogAPI.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{

    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
