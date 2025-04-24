using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;

namespace StoreCatalogAPI.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{

    IEnumerable<Produto> GetProdutosPorCategoria(int id);

    PagedList<Produto> GetProdutosPaginados(ProdutosParameters produtosParams);

}
