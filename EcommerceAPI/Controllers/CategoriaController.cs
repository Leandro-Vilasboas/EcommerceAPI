using EcommerceAPI.Data.CategoriaDtos;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Interfaces.Services;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _logger = logger;
        }

        /// <summary>
        /// Lista os itens da To-do list.
        /// </summary>
        /// <returns>Os itens da To-do list</returns>
        /// <response code="200">Returna os itens da To-do list cadastrados</response>
        [SwaggerOperation("Endpoint para cadastrar uma Categoria")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {
            Log.Information("Iniciando o cadastro de Categoria");
            var result = await _categoriaService.CadastrarCategoria(categoriaDto);
            if (result == null)
            {
                return BadRequest("O nome informado já existe");
            }
            return Ok(result);
        }

        [SwaggerOperation("Endpoint para filtrar uma Categoria por Id")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> FiltrarPorId(int id)
        {
            Log.Information("Iniciando filtro de categoria por Id");
            var result = await _categoriaService.BuscarPorId(id);
            if(result == null)
            {
                return BadRequest("A categoria informada não existe");
            }
            return Ok(result);
        }

        [SwaggerOperation("Endpoint para filtrar uma Categoria por ordem ('desc' ou 'asc') e/ou por nome")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult FiltrarCategorias([FromQuery] FiltroCategoriaDto filtro)
        {
            Log.Information("Iniciando filtros variados na Categoria");
            /*var verificaNomeETamanho = _categoriaService.ValidarNomeETamanho(filtro.NomeCat);
            if(verificaNomeETamanho == false)
            {
                return BadRequest("Insira um nome com no mínimo 3 caracteres");
            }*/

            var validarOrdem = _categoriaService.ValidarOrdem(filtro.OrdemCat);
            if(validarOrdem == false)
            {
                return BadRequest("Ordem precisa ser asc ou desc");
            }

            var result = _categoriaService.FiltrarCategorias(filtro);
            if(result == null)
            {
                return BadRequest("Insira um filtro para consulta");
            }
            return Ok(result);
        }

        [SwaggerOperation("Endpoint para editar o nome de uma Categoria")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarNomeCategoria(int id, [FromBody] UpdateCategoriaDto updateCategoria)
        {
            Log.Information("Iniciando edição de nome da Categoria");
            var nomeEditado = await _categoriaService.EditarNome(id, updateCategoria);
            if (nomeEditado == null)
            {
                return BadRequest("A categoria informada não existe ou já foi criada com o mesmo nome");
            }
            return Ok(nomeEditado);
        }

        [SwaggerOperation("Endpoint para editar o status de uma Categoria")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditarStatusCategoria(int id)
        {
            Log.Information("Iniciando edição de status da categoria");
            var statusEditado = await _categoriaService.EditarStatus(id);
            if (statusEditado == null)
            {
                return BadRequest("A categoria informada não existe");
            }
            return Ok(statusEditado);
        }

        [SwaggerOperation("Endpoint para deletar uma Categoria")]
        [ProducesResponseType(typeof(CategoriaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCategoria(int id)
        {
            Log.Information("Deletanto categoria informada por ID");
            var delete = await _categoriaService.DeletarCategoria(id);
            return Ok(delete);
        }
    }
}
