using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.SubcategoriaDtos;
using EcommerceAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Interfaces.Repository
{
    public interface ISubcategoriaRepository
    {
        Task<SubcategoriaModel> AdicionarSubcategoria(SubcategoriaModel subcategoria);
        Task<CategoriaModel> ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto);
        Task<SubcategoriaModel> VerificarNomeExiste(CreateSubcategoriaDto subcategoriaDto);
        Task<SubcategoriaModel> VerificarId(int id);
        IQueryable<SubcategoriaModel> FiltrarTodos();
        IQueryable<SubcategoriaModel> FiltrarPorNome(FiltroSubcategoriaDto filtro);
        Task<SubcategoriaModel> EditarNome(UpdateSubcategoriaDto updateSubcategoria);
        void DeletarSubcategoria(SubcategoriaModel subcategoria);
        void Salvar();
    }
}
