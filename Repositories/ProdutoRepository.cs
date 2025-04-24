using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;

namespace StoreCatalogAPI.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }

    public PagedList<Produto> GetProdutosPaginados(ProdutosParameters produtosParams)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos,
            produtosParams.PageNumber, produtosParams.PageSize);

        return produtosOrdenados;
    }
}
