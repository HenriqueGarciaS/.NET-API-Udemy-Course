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
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork; 
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(id);

            if(categoria == null) 
                return NotFound("Categoria não encontrada");

            return categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();
                
                return Ok(categorias);
        }
        

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria == null) return BadRequest();

            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new {id = categoriaCriada.Id }, categoriaCriada);

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put([FromBody] Categoria categoria, int id)
        {
            if (id != categoria.Id)
                return BadRequest();
            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok(categoria);
        }

    }
}
