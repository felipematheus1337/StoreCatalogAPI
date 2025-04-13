using Microsoft.AspNetCore.Mvc;
using StoreCatalogAPI.Context;
using StoreCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StoreCatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos.ToList();

        if (produtos is null) return NotFound("Produtos não encontrados.");

        return produtos;
    }

    [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        return produto;
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Produto>> getProdutoByName([BindRequired] string name)
    {
        return await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == name);

    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> GetOneAsync(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null) return NotFound();

        return produto;
    }


    [HttpPost]
    public ActionResult Post(Produto produto)
    {

        if (produto is null) return BadRequest();

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);

    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null) return NotFound("Produto não localizado.");


        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok();


    }
    
}
 

