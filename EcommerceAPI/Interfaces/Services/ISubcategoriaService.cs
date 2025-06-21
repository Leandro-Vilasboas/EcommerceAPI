using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.SubcategoriaDtos;
using EcommerceAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces.Services
{
    public interface ISubcategoriaService
    {
        Task<SubcategoriaModel> CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDto);
        Task<CategoriaModel> ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto);
        Task<CategoriaModel> CompararNomeCatComSub(CreateSubcategoriaDto subcategoriaDto);
        Task<SubcategoriaModel> BuscarPorId(int id);
        IQueryable<SubcategoriaModel> FiltrarSubcategorias(FiltroSubcategoriaDto filtro, PaginacaoDto paginacao);
        bool ValidarNomeETamanho(string nomeCat);
        bool ValidarOrdem(string ordem);
        Task<SubcategoriaModel> EditarNome(int id, UpdateSubcategoriaDto updateSubcategoria);
        Task<SubcategoriaModel> EditarStatus(int id);
        Task<SubcategoriaModel> DeletarSubcategoria(int id);
    }
}
