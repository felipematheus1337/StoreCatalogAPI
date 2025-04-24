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
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _uof.CategoriaRepository.GetAllAsync();
            return Ok(_mapper.Map<CategoriaDTO>(categorias));


        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {

            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
            if (categoria is null) return NotFound();

            return Ok(_mapper.Map<CategoriaDTO>(categoria));

        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriaParameters)
        {

            var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(categoriaParameters);

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
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
        {

            if (categoriaDto is null) return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            await _uof.CommitAsync();

            var categoriaDtoCriada = _mapper.Map<CategoriaDTO>(categoriaCriada);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDtoCriada.CategoriaId }, categoriaDtoCriada);

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId) return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Update(categoria);
            await _uof.CommitAsync();

            return Ok(categoriaDto);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
            if (categoria is null) return NotFound("Categoria não localizada.");


            var categoriaExcluida =  _uof.CategoriaRepository.Delete(categoria);
            await _uof.CommitAsync();

            return Ok(categoriaExcluida);


        }

    }
}
