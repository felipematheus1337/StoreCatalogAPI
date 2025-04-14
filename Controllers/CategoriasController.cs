using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreCatalogAPI.Filter;
using StoreCatalogAPI.Models;
using StoreCatalogAPI.Repositories;

namespace StoreCatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IRepository<Categoria> _repository;

        public CategoriasController(ICategoriaRepository repo)
        {
            _repository = repo;
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetAll();
            return Ok(categorias);


        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            var categoria = _repository.Get(c => c.CategoriaId == id);
            if (categoria is null) return NotFound();

            return Ok(categoria);

        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {

            if (categoria is null) return BadRequest();

            var categoriaCriada =  _repository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);

        }


        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId) return BadRequest();

            _repository.Update(categoria);
            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.Get(c => c.CategoriaId == id);
            if (categoria is null) return NotFound("Categoria não localizada.");


            var categoriaExcluida = _repository.Delete(categoria);

            return Ok(categoriaExcluida);


        }

    }
}
