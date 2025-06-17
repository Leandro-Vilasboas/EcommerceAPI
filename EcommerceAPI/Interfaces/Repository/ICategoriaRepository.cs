using EcommerceAPI.Data.CategoriaDtos;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces.Reposity
{
    public interface ICategoriaRepository
    {
        Task<CategoriaModel> AdicionarCategoria(CategoriaModel categoria);
        IQueryable<CategoriaModel> FiltrarTodos();
        IQueryable<CategoriaModel> FiltrarPorNome(FiltroCategoriaDto filtro);
        Task<CategoriaModel> VerificarId(int id);
        Task<CategoriaModel> VerficarNomeExiste(CategoriaModel categoria);
        Task<CategoriaModel> EditarNome(UpdateCategoriaDto updateCategoria);
        IQueryable<SubcategoriaModel> ValidarCategoriaId(int id);
        void DeletarCategoria(CategoriaModel categoria);
        void Salvar();
    }
}
