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
    private readonly IRepository<Produto> _repository;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository repo, IProdutoRepository produtoRepository)
    {
        _repository = repo;
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetAll();

        if (produtos is null) return NotFound("Produtos não encontrados.");

        return Ok(produtos);
    }

    [HttpGet("produtos/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetByCategoriaId(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);

        if (produtos is null) return NotFound($"Produtos não encontrados com a categoria: {id}.");

        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.Get(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        return produto;
    }

    [HttpPost]
    public ActionResult Post(Produto produto)
    {

        if (produto is null) return BadRequest();

        var novoProduto = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();

        var produtoAtualizado = _repository.Update(produto);

        if (produtoAtualizado is not null) return Ok(produto);

        return StatusCode(500, $"Falha ao atualizar o produto com id {id}");

    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {

        var produto = _repository.Get(p => p.ProdutoId == id);
        var produtoRemovido = _repository.Delete(produto);

        if (produtoRemovido is not null) return Ok($"Produto de id: {id} foi removido.");

        return StatusCode(500, $"Falha ao remover produto com o id: {id}");


    }
    
}
 

