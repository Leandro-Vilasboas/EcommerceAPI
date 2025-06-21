using AutoMapper;
using EcommerceAPI.Data.CategoriaDtos;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Interfaces.Reposity;
using EcommerceAPI.Interfaces.Services;
using EcommerceAPI.Models;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(IMapper mapper, ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaModel> CadastrarCategoria(CreateCategoriaDto categoriaDto)
        {
            CategoriaModel categoria = _mapper.Map<CategoriaModel>(categoriaDto);

            var nomeExiste = await _categoriaRepository.VerficarNomeExiste(categoria);
            if (nomeExiste == null)
            {
                return await _categoriaRepository.AdicionarCategoria(categoria);
            }
            Log.Information("O nome informado já existe");
            return null;
        }
        
        public async Task<CategoriaModel> BuscarPorId(int id)
        {
            var verificarId = await _categoriaRepository.VerificarId(id);
            if (verificarId != null)
            {
                ReadCategoriaDto categoriaDto = _mapper.Map<ReadCategoriaDto>(verificarId);
            }
            Log.Information("A categoria buscada não existe.");
            return verificarId;
        }

        public IQueryable<CategoriaModel> FiltrarCategorias(FiltroCategoriaDto filtro)
        {
            IQueryable<CategoriaModel> categorias = null;

            //mostrar filtros por nome           
            if (filtro.NomeCat != null && filtro.NomeCat.Length >= 3)
            {
                categorias = _categoriaRepository.FiltrarPorNome(filtro);
            }
            else
            {
                categorias = _categoriaRepository.FiltrarTodos();
            }

            //mostrar filtros por status
            if (filtro.StatusCat != null)
            {
                categorias = categorias.Where(c => c.Status == filtro.StatusCat);
            }

            //mostrar filtros por ordem descendente
            if (filtro.OrdemCat == "desc")
            {
                categorias = categorias.OrderByDescending(c => c.Nome);
            }
            if (filtro.OrdemCat == "asc")
            {
                categorias = categorias.OrderBy(c => c.Nome);
            }
            return categorias;
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

        public async Task<CategoriaModel> EditarNome(int id, UpdateCategoriaDto updateCategoria)
        {
            var categoria = await _categoriaRepository.VerificarId(id);
            var nomeExiste = await _categoriaRepository.EditarNome(updateCategoria);
            if (categoria == null)
            {
                return null;
            }
            if (nomeExiste != null)
            {
                return null;
            }
            _mapper.Map(updateCategoria, categoria);
            categoria.DataDeAlteracao = DateTime.Now;
            _categoriaRepository.Salvar();
            return categoria;
        }

        public async Task<CategoriaModel> EditarStatus(int id)
        {
            var categoria = await _categoriaRepository.VerificarId(id);
            var subcategoria = _categoriaRepository.ValidarCategoriaId(id);
            if (categoria == null)
            {
                return null;
            }
            if (categoria.Status == true)
            {
                categoria.Status = false;
                categoria.DataDeAlteracao = DateTime.Now;
                foreach (SubcategoriaModel subcat in subcategoria)
                {
                    subcat.Status = false;
                    subcat.DataDeAlteracao = DateTime.Now;
                }
            }
            else
            {
                categoria.Status = true;
                categoria.DataDeAlteracao = DateTime.Now;
                foreach (SubcategoriaModel subcat in subcategoria)
                {
                    subcat.Status = true;
                    subcat.DataDeAlteracao = DateTime.Now;
                }
            }
            _categoriaRepository.Salvar();
            return categoria;
        }

        public async Task<CategoriaModel> DeletarCategoria(int id)
        {
            var categoria = await _categoriaRepository.VerificarId(id);
            if(categoria == null)
            {
                return null;
            }
            _categoriaRepository.DeletarCategoria(categoria);
            return null;
        }
    }
}
