using Microsoft.AspNetCore.Mvc;
using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreCatalogAPI.Repositories;

namespace StoreCatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

        if (produtos is null) return NotFound("Produtos não encontrados.");

        return Ok(produtos);
    }

    [HttpGet("produtos/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetByCategoriaId(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if (produtos is null) return NotFound($"Produtos não encontrados com a categoria: {id}.");

        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {

        if (produto is null) return BadRequest();
y.
        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        if (produtoAtualizado is not null) return Ok(produto);

        return StatusCode(500, $"Falha ao atualizar o produto com id {id}");

    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        var produtoRemovido = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        if (produtoRemovido is not null) return Ok($"Produto de id: {id} foi removido.");

        return StatusCode(500, $"Falha ao remover produto com o id: {id}");


    }
    
}
 

