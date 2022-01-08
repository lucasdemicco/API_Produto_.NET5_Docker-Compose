using EFCoreSqlServer.Data;
using EFCoreSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreSqlServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly ContextProdutos _context;
        public ProdutosController(ContextProdutos context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduct()
        {
            if (!ModelState.IsValid)
                return StatusCode(statusCode: 404, "Produtos não encontrados");

            return Ok(await _context.Produtos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Create(Produto produto)
        {
            if(!ModelState.IsValid)
                return StatusCode(statusCode: 500, "Erro para inserir na base de dados");

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Update(int id, Produto produtoFromJson)
        {
            if (!ModelState.IsValid)
                return StatusCode(statusCode: 500, "Erro para consultar na base de dados");

            var produtos = await _context.Produtos.FindAsync(id);

            if(produtos == null)
            {
                return StatusCode(statusCode: 404, "Produtos não encontrados");
            }

            produtos.Name = produtoFromJson.Name;
            produtos.Price = produtoFromJson.Price;
            produtos.Description = produtoFromJson.Description;

            await _context.SaveChangesAsync();
            return Ok(produtos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);

            if (produtos == null)
                return StatusCode(statusCode: 404, "produtos não encontrados");

            _context.Remove(produtos);
            await _context.SaveChangesAsync();
            return Ok(produtos);
        }
    }
}
