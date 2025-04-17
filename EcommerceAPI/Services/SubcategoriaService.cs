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

namespace EcommerceAPI.Services
{
    public class SubcategoriaService
    {
        private readonly IMapper _mapper;
        private readonly SubcategoriaRepository _subcategoriaRepository;

        public SubcategoriaService(SubcategoriaRepository subcategoriaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public SubcategoriaModel CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDto)
        {
            var nomeExiste = _subcategoriaRepository.VerificarNomeExiste(subcategoriaDto);
            SubcategoriaModel subcategoria = _mapper.Map<SubcategoriaModel>(subcategoriaDto);
            if (nomeExiste == null)
            {
                return _subcategoriaRepository.AdicionarSubcategoria(subcategoria);                
            }
            return null;
        }

        public CategoriaModel ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto)
        {
            var subcategoria = _subcategoriaRepository.ValidarCategoriaId(subcategoriaDto);
            if(subcategoria == null)
            {
                return null;
            }
            return subcategoria;
        }

        public CategoriaModel CompararNomeCatComSub(CreateSubcategoriaDto subcategoriaDto)
        {
            var categoria = _subcategoriaRepository.ValidarCategoriaId(subcategoriaDto);
            if (categoria.Nome.Equals(subcategoriaDto.Nome))
            {
                return null;
            }
            return categoria;
        }

        public SubcategoriaModel BuscarPorId(int id)
        {
            var verificarId = _subcategoriaRepository.VerificarId(id);
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
                subcategorias = _subcategoriaRepository.FiltrarPorNome(filtro)
                    .Skip(paginacao.ItensPagina * paginacao.Pagina).Take(paginacao.ItensPagina);
            }
            else
            {
                subcategorias = _subcategoriaRepository.FiltrarTodos()
                    .Skip(paginacao.ItensPagina * paginacao.Pagina).Take(paginacao.ItensPagina);
            }

            //mostrar filtros por status
            if (filtro.StatusSub != null)
            {
                subcategorias = subcategorias.Where(c => c.Status == filtro.StatusSub)
                    .Skip(paginacao.ItensPagina * paginacao.Pagina).Take(paginacao.ItensPagina);
            }

            //mostrar filtros por ordem descendente
            if (filtro.OrdemSub == "desc")
            {
                subcategorias = subcategorias.OrderByDescending(c => c.Nome)
                    .Skip(paginacao.ItensPagina * paginacao.Pagina).Take(paginacao.ItensPagina);
            }
            if (filtro.OrdemSub == "asc")
            {
                subcategorias = subcategorias.OrderBy(c => c.Nome)
                    .Skip(paginacao.ItensPagina * paginacao.Pagina).Take(paginacao.ItensPagina);
            }
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

        public SubcategoriaModel EditarNome(int id, UpdateSubcategoriaDto updateSubcategoria)
        {
            var subcategoria = _subcategoriaRepository.VerificarId(id);
            var nomeExiste = _subcategoriaRepository.EditarNome(updateSubcategoria);
            if (subcategoria == null)
            {
                return null;
            }
            if (nomeExiste != null)
            {
                return null;
            }
            _mapper.Map(updateSubcategoria, subcategoria);
            subcategoria.DataDeAlteração = DateTime.Now;
            _subcategoriaRepository.Salvar();
            return subcategoria;
        }

        public SubcategoriaModel EditarStatus(int id)
        {
            var subcategoria = _subcategoriaRepository.VerificarId(id);
            if (subcategoria == null)
            {
                return null;
            }
            if (subcategoria.Status == true)
            {
                subcategoria.Status = false;
                subcategoria.DataDeAlteração = DateTime.Now;
            }
            else
            {
                subcategoria.Status = true;
                subcategoria.DataDeAlteração = DateTime.Now;
            }
            _subcategoriaRepository.Salvar();
            return subcategoria;
        }

        public SubcategoriaModel DeletarSubcategoria(int id)
        {
            var subcategoria = _subcategoriaRepository.VerificarId(id);
            if (subcategoria == null)
            {
                return null;
            }
            _subcategoriaRepository.DeletarSubcategoria(subcategoria);
            return subcategoria;
        }
    }
}
