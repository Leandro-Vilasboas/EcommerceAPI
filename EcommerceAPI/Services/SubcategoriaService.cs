using Serilog;
using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using EcommerceAPI.Data.SubcategoriaDtos;
using System.Linq;
using System;
using System.Threading.Tasks;
using EcommerceAPI.Interfaces.Services;

namespace EcommerceAPI.Services
{
    public class SubcategoriaService : ISubcategoriaService
    {
        private readonly IMapper _mapper;
        private readonly SubcategoriaRepository _subcategoriaRepository;

        public SubcategoriaService(SubcategoriaRepository subcategoriaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<SubcategoriaModel> CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDto)
        {
            var nomeExiste = await _subcategoriaRepository.VerificarNomeExiste(subcategoriaDto);
            SubcategoriaModel subcategoria = _mapper.Map<SubcategoriaModel>(subcategoriaDto);
            if (nomeExiste == null)
            {
                return await _subcategoriaRepository.AdicionarSubcategoria(subcategoria);                
            }
            return null;
        }

        public async Task<CategoriaModel> ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto)
        {
            var subcategoria = await _subcategoriaRepository.ValidarCategoriaId(subcategoriaDto);
            if(subcategoria == null)
            {
                return null;
            }
            return subcategoria;
        }

        public async Task<CategoriaModel> CompararNomeCatComSub(CreateSubcategoriaDto subcategoriaDto)
        {
            var categoria = await _subcategoriaRepository.ValidarCategoriaId(subcategoriaDto);
            if (categoria.Nome.Equals(subcategoriaDto.Nome))
            {
                return null;
            }
            return categoria;
        }

        public async Task<SubcategoriaModel> BuscarPorId(int id)
        {
            var verificarId = await _subcategoriaRepository.VerificarId(id);
            if (verificarId != null)
            {
                ReadSubcategoriaDto subcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(verificarId);
            }
            Log.Information("A subcategoria buscada não existe.");
            return verificarId;
        }

        public IQueryable<SubcategoriaModel> FiltrarSubcategorias(FiltroSubcategoriaDto filtro, PaginacaoDto paginacao)
        {
            IQueryable<SubcategoriaModel> subcategorias = null;

            //mostrar filtros por nome           
            if (filtro.NomeSub != null && filtro.NomeSub.Length >= 3)
            {
                subcategorias = _subcategoriaRepository.FiltrarPorNome(filtro).ToList().AsQueryable();
            }
            else
            {
                subcategorias = _subcategoriaRepository.FiltrarTodos().ToList().AsQueryable();
            }

            //mostrar filtros por status
            if (filtro.StatusSub != null)
            {
                subcategorias = subcategorias.Where(c => c.Status == filtro.StatusSub);
            }

            //mostrar filtros por ordem descendente
            if (filtro.OrdemSub == "desc")
            {
                subcategorias = subcategorias.OrderByDescending(c => c.Nome);
            }
            if (filtro.OrdemSub == "asc")
            {
                subcategorias = subcategorias.OrderBy(c => c.Nome);
            }

            subcategorias = subcategorias
            .Skip(paginacao.ItensPagina * paginacao.Pagina)
            .Take(paginacao.ItensPagina);
            return subcategorias;
        }

        public bool ValidarNomeETamanho(string nomeCat)
        {
            if (nomeCat != null && nomeCat.Length < 3)
            {
                return false;
            }
            return true;
        }

        public bool ValidarOrdem(string ordem)
        {
            if (ordem != "asc" && ordem != "desc" && ordem != null)
            {
                return false;
            }
            return true;
        }

        public async Task<SubcategoriaModel> EditarNome(int id, UpdateSubcategoriaDto updateSubcategoria)
        {
            var subcategoria = await _subcategoriaRepository.VerificarId(id);
            var nomeExiste = await _subcategoriaRepository.EditarNome(updateSubcategoria);
            if (subcategoria == null)
            {
                return null;
            }
            if (nomeExiste != null)
            {
                return null;
            }
            _mapper.Map(updateSubcategoria, subcategoria);
            subcategoria.DataDeAlteracao = DateTime.Now;
            _subcategoriaRepository.Salvar();
            return subcategoria;
        }

        public async Task<SubcategoriaModel> EditarStatus(int id)
        {
            var subcategoria = await _subcategoriaRepository.VerificarId(id);
            if (subcategoria == null)
            {
                return null;
            }
            if (subcategoria.Status == true)
            {
                subcategoria.Status = false;
                subcategoria.DataDeAlteracao = DateTime.Now;
            }
            else
            {
                subcategoria.Status = true;
                subcategoria.DataDeAlteracao = DateTime.Now;
            }
            _subcategoriaRepository.Salvar();
            return subcategoria;
        }

        public async Task<SubcategoriaModel> DeletarSubcategoria(int id)
        {
            var subcategoria = await _subcategoriaRepository.VerificarId(id);
            if (subcategoria == null)
            {
                return null;
            }
            _subcategoriaRepository.DeletarSubcategoria(subcategoria);
            return subcategoria;
        }
    }
}
