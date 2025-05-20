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
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();

            if (produtos == null)
                return NotFound("Produtos não encontrados");

            return Ok(produtos);

            
        }

        [HttpGet("Produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
            var produtos =  _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);
            if (produtos == null)
                return NotFound();
            
            return Ok(produtos);

        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.GetById(id);
            
            if (produto == null) return NotFound("Produto não encontrado");

            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();

            var produtoCriado = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put([FromBody] Produto produto, int id) 
        {
            if (id != produto.Id)
                return BadRequest();
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok(produto);
        }
    }
}
