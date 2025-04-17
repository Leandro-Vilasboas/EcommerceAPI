using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.ProdutoDtos;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        private readonly ProdutoRepository _produtoRepository;
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context, ProdutoService produtoService, ProdutoRepository produtoRepository)
        {
            _context = context;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        public IActionResult CadastrarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            ReadProdutoDto readDto = _produtoService.AdicionarProduto(produtoDto);
            if(readDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(FiltrarProdutos), new { Id = readDto.Id }, readDto);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> FiltrarPorId(int id)
        {
            var filtrarPorId = await _produtoRepository.PesquisarPorId(id);
            if (filtrarPorId != null)
            {
                return Ok(filtrarPorId);
            }
            return NotFound("O produto buscado não existe.");
        }

        [HttpGet]
        public async Task<IActionResult> FiltrarProdutos([FromQuery] FiltroProdutoDto filtro, [FromQuery] PaginacaoDto paginacao, string ordem)
        {
            var pesquisaPorFiltros = await _produtoRepository.PesquisarPorFiltros(filtro, paginacao, ordem);
            if (pesquisaPorFiltros != null)
            {
                return Ok(pesquisaPorFiltros);
            }
            return NotFound("O produto buscado não existe.");
        }

        [HttpPut("{id}")]
        public IActionResult EditarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            UpdateProdutoDto updateDto = _produtoService.ModificarProduto(id, produtoDto);
            if (updateDto == null)
            {
                return Ok(produtoDto);
            }
            return NotFound("O produto a ser editado não existe");
        }

        [HttpPatch("{id}")]
        public IActionResult EditarStatusProduto(int id)
        {
            UpdateProdutoDto updateDto = _produtoService.ModificarStatus(id);
            return Ok(updateDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarProduto(int id)
        {
            ReadProdutoDto produtoDto = _produtoService.DeletarProduto(id);
            return Ok();
        }
    }
}

