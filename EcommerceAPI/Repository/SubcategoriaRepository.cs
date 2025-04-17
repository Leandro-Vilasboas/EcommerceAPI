using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.SubcategoriaDtos;
using EcommerceAPI.Models;
using System.Linq;

namespace EcommerceAPI.Repository
{
    public class SubcategoriaRepository
    {

        private readonly AppDbContext _context;

        public SubcategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public SubcategoriaModel AdicionarSubcategoria(SubcategoriaModel subcategoria)
        {
            _context.Subcategorias.Add(subcategoria);
            _context.SaveChanges();
            return subcategoria;
        }

        public CategoriaModel ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto)
        {
            var categoriaId = _context.Categorias.FirstOrDefault(c => c.Id == subcategoriaDto.CategoriaId);
            return categoriaId;
        }
        
        public SubcategoriaModel VerificarNomeExiste(CreateSubcategoriaDto subcategoriaDto)
        {
            var nomeExistente = _context.Subcategorias.FirstOrDefault(c => c.Nome == subcategoriaDto.Nome);
            return nomeExistente;
        }

        public SubcategoriaModel VerificarId(int id)
        {
            var subcategoria = _context.Subcategorias.FirstOrDefault(c => c.Id == id);
            return subcategoria;
        }

        public IQueryable<SubcategoriaModel> FiltrarTodos()
        {
            var subcategorias = _context.Subcategorias;
            return subcategorias;
        }

        public IQueryable<SubcategoriaModel> FiltrarPorNome(FiltroSubcategoriaDto filtro)
        {
            var subcategorias = _context.Subcategorias.Where(c => c.Nome.ToLower().Contains(filtro.NomeSub.ToLower()));
            return subcategorias;
        }

        public SubcategoriaModel EditarNome(UpdateSubcategoriaDto updateSubcategoria)
        {
            var editar = _context.Subcategorias.FirstOrDefault(c => c.Nome == updateSubcategoria.Nome);
            return editar;
        }

        public void DeletarSubcategoria(SubcategoriaModel subcategoria)
        {
            _context.Subcategorias.Remove(subcategoria);
            _context.SaveChanges();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
