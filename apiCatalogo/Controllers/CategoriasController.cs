using apiCatalogo.context;
using apiCatalogo.models;
using apiCatalogo.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IRepository<Categoria> _repository;

        public CategoriasController(IRepository<Categoria> repository) 
        {
            _repository = repository; 
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _repository.GetById(id);

            if(categoria == null) 
                return NotFound("Categoria não encontrada");

            return categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetAll();
                
                return Ok(categorias);
        }
        

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria == null) return BadRequest();

            var categoriaCriada = _repository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new {id = categoriaCriada.Id }, categoriaCriada);

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put([FromBody] Categoria categoria, int id)
        {
            if (id != categoria.Id)
                return BadRequest();
            _repository.Update(categoria);

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.Delete(id);

            return Ok(categoria);
        }

    }
}
