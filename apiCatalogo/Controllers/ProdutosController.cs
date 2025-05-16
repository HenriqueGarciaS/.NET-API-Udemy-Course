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

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAsync()
        {
            var produtos = _produtoRepository.GetProdutos();

            if (produtos == null)
                return NotFound("Produtos não encontrados");

            return Ok(produtos);

            
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.GetProdutoById(id);
            
            if (produto == null) return NotFound("Produto não encontrado");

            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();

            var produtoCriado = _produtoRepository.CreateProduto(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.Id }, produtoCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put([FromBody] Produto produto, int id) 
        {
            if (id != produto.Id)
                return BadRequest();
            _produtoRepository.UpdateProduto(produto);

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _produtoRepository.DeleteProduto(id);

            return Ok(produto);
        }
    }
}
