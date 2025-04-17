using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.CDDtos;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CDController : ControllerBase
    {
        private readonly CDService _cdService;
        private readonly CDRepository _cdRepository;
        private readonly AppDbContext _context;        

        public CDController(AppDbContext context, CDService cdService, CDRepository cdRepository)
        {
            _context = context;
            _cdService = cdService;
            _cdRepository = cdRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCentroDeDistribuicao([FromBody] CreateCDDto cdDto)
        {
            var cadastrarCD = await _cdService.CadastrarCD(cdDto);
            if (cadastrarCD == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(FiltrarCentroDeDistribuicao), new { Id = cadastrarCD.Id }, cadastrarCD);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> FiltrarCentroDeDistribuicaoPorId(int id)
        {
            var filtrarPorId = await _cdRepository.PesquisarPorId(id);
            if (filtrarPorId != null)
            {
                return Ok(filtrarPorId);
            }
            return NotFound("O centro de distribuição buscado não existe.");
        }

        [HttpPost("/Filtro")]
        public async Task<IActionResult> FiltrarCentroDeDistribuicao([FromBody] FiltroCDDto filtro)
        {
            var pesquisaPorFiltros = await _cdRepository.PesquisarPorFiltros(filtro);
            if (pesquisaPorFiltros != null)
            {
                return Ok(pesquisaPorFiltros);
            }
            return NotFound("O centro de distribuição buscado não existe.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCentroDeDistribuicao(int id, [FromBody] UpdateCDDto cdDto)
        {
            var updateDto = await _cdService.EditarCD(id, cdDto);
            if (updateDto != null)
            {
                return Ok(cdDto);
            }
            return NotFound("O centro de distribuição a ser editado não existe ou já está cadastrado");
        }

        [HttpPatch("{id}")]
        public IActionResult EditarStatusCentroDeDistribuicao(int id)
        {
            UpdateCDDto updateDto = _cdService.ModificarStatus(id);
            return Ok(updateDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCentroDeDistribuicao(int id)
        {
            ReadCDDto cdDto = _cdService.DeletarCD(id);
            return Ok();
        }
    }
}
