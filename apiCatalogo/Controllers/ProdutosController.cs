using apiCatalogo.context;
using apiCatalogo.models;
using apiCatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IRepository<Produto> _repository;

        public ProdutosController(IProdutoRepository produtoRepository, IRepository<Produto> repository)
        {
            _produtoRepository = produtoRepository;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetAll();

            if (produtos == null)
                return NotFound("Produtos não encontrados");

            return Ok(produtos);

            
        }

        [HttpGet("Produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
            var produtos =  _produtoRepository.GetProdutosPorCategoria(id);
            if (produtos == null)
                return NotFound();
            
            return Ok(produtos);

        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.GetById(id);
            
            if (produto == null) return NotFound("Produto não encontrado");

            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();

            var produtoCriado = _repository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put([FromBody] Produto produto, int id) 
        {
            if (id != produto.Id)
                return BadRequest();
            _repository.Update(produto);

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _repository.Delete(id);

            return Ok(produto);
        }
    }
}
