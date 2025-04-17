using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.SubcategoriaDtos;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubcategoriaController : ControllerBase
    {
        private readonly SubcategoriaService _subcategoriaService;
        private readonly ILogger<CategoriaController> _logger;

        public SubcategoriaController(SubcategoriaService subcategoriaService, ILogger<CategoriaController> logger)
        {
            _subcategoriaService = subcategoriaService;
            _logger = logger;
        }
        
        [HttpPost]
        public IActionResult CadastrarSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
        {
            Log.Information("Iniciando Cadastro de Subcategodia");
            var validarCetegoriaId = _subcategoriaService.ValidarCategoriaId(subcategoriaDto);
            if(validarCetegoriaId == null)
            {
                return BadRequest("A categoria informada não existe");
            }

            var validarCategoriaStatus = _subcategoriaService.ValidarCategoriaId(subcategoriaDto);
            if(validarCategoriaStatus.Status == false)
            {
                return BadRequest("A categoria selecionada está inativa, reative para cadastrar uma subcategoria.");
            }

            var compararNomeCatComSub = _subcategoriaService.CompararNomeCatComSub(subcategoriaDto);
            if (compararNomeCatComSub == null)
            {
                return BadRequest("O nome da subcategoria já está sendo utilizado por uma categoria, por favor utilize outro nome.");
            }

            var result = _subcategoriaService.CadastrarSubcategoria(subcategoriaDto);
            if (result == null)
            {
                return BadRequest("O nome informado já está sendo utilizado, por favor, digite um nome diferente.");                 
            }
            return Ok(result);
        }
        
        [HttpGet("{Id}")]
        public IActionResult FiltrarPorId(int id)
        {
            Log.Information("Iniciando filtro de subcategoria por Id");
            var result = _subcategoriaService.BuscarPorId(id);
            if (result == null)
            {
                return BadRequest("A Subcategoria informada não existe");
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult FiltrarSubcategorias([FromQuery] FiltroSubcategoriaDto filtro, [FromQuery] PaginacaoDto paginacao)
        {

            Log.Information("Iniciando filtros variados na Subcategoria");
            var verificaNomeETamanho = _subcategoriaService.ValidarNomeETamanho(filtro.NomeSub);
            if (verificaNomeETamanho == false)
            {
                return BadRequest("Insira um nome com no mínimo 3 caracteres");
            }

            var validarOrdem = _subcategoriaService.ValidarOrdem(filtro.OrdemSub);
            if (validarOrdem == false)
            {
                return BadRequest("Ordem precisa ser asc ou desc");
            }

            var result = _subcategoriaService.FiltrarSubcategorias(filtro, paginacao);
            if (result == null)
            {
                return BadRequest("Insira um filtro para consulta");
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult EditarNomeSubcategoria(int id, [FromBody] UpdateSubcategoriaDto updateSubcategoria)
        {
            Log.Information("Iniciando edição de nome da Subcategoria");
            var nomeEditado = _subcategoriaService.EditarNome(id, updateSubcategoria);
            if (nomeEditado == null)
            {
                return BadRequest("A subcategoria informada não existe ou já foi criada com o mesmo nome");
            }
            return Ok(nomeEditado);
        }

        [HttpPatch("{id}")]
        public IActionResult EditarStatusSubcategoria(int id)
        {
            Log.Information("Iniciando edição de status da subcategoria");
            var statusEditado = _subcategoriaService.EditarStatus(id);
            if (statusEditado == null)
            {
                return BadRequest("A subcategoria informada não existe");
            }
            return Ok(statusEditado);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSubcategoria(int id)
        {
            Log.Information("Deletanto subcategoria informada por ID");
            var delete = _subcategoriaService.DeletarSubcategoria(id);
            return Ok(delete);
        }
    }
}