using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreCatalogAPI.DTOs;
using StoreCatalogAPI.Filter;
using StoreCatalogAPI.Models;
using StoreCatalogAPI.Pagination;
using StoreCatalogAPI.Repositories;

namespace StoreCatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _uof.CategoriaRepository.GetAll();
            return Ok(_mapper.Map<CategoriaDTO>(categorias));


        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {

            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null) return NotFound();

            return Ok(_mapper.Map<CategoriaDTO>(categoria));

        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriasParameters categoriaParameters)
        {

            var categorias = _uof.CategoriaRepository.GetCategorias(categoriaParameters);

            if (categorias is null) return NoContent();

            var metaData = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious,
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metaData));

            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

            return Ok(categoriasDto);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {

            if (categoriaDto is null) return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            var categoriaDtoCriada = _mapper.Map<CategoriaDTO>(categoriaCriada);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDtoCriada.CategoriaId }, categoriaDtoCriada);

        }


        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId) return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            return Ok(categoriaDto);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null) return NotFound("Categoria não localizada.");


            var categoriaExcluida =  _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            return Ok(categoriaExcluida);


        }

    }
}
