using EcommerceAPI.Data.CategoriaDtos;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaModel> CadastrarCategoria(CreateCategoriaDto categoriaDto);
        Task<CategoriaModel> BuscarPorId(int id);
        IQueryable<CategoriaModel> FiltrarCategorias(FiltroCategoriaDto filtro);
        bool ValidarOrdem(string ordem);
        Task<CategoriaModel> EditarNome(int id, UpdateCategoriaDto updateCategoria);
        Task<CategoriaModel> EditarStatus(int id);
        Task<CategoriaModel> DeletarCategoria(int id);
    }
}
