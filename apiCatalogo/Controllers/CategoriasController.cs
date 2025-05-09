﻿using apiCatalogo.context;
using apiCatalogo.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context) 
        {
            _context = context; 
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if(categoria == null) 
                return NotFound("Categoria não encontrada");

            return categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

                if (categorias == null)
                    return NotFound("Categorias não encontradas");

                return categorias;
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a solicitação");
            
            }
            
        }

       

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

            if (categorias == null)
                return NotFound("Categorias não encontradas");

            return categorias;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria == null) return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new {id = categoria.Id }, categoria);

        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put([FromBody] Categoria categoria, int id)
        {
            if (id != categoria.Id)
                return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id); ;

            if (categoria == null) return NotFound("Categoria não encontrado");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

    }
}
