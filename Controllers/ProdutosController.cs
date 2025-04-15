using Microsoft.AspNetCore.Mvc;
using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreCatalogAPI.Repositories;
using StoreCatalogAPI.DTOs;
using AutoMapper;

namespace StoreCatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

        if (produtos is null) return NotFound("Produtos não encontrados.");

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("produtos/{id:int}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetByCategoriaId(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if (produtos is null) return NotFound($"Produtos não encontrados com a categoria: {id}.");

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {

        if (produtoDto is null) return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);
y.
        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId) return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var produtoDtoAtualizado = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        if (produtoAtualizado is not null) return Ok(produtoDtoAtualizado);

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
 

