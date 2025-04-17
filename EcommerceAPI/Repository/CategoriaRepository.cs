using System.Linq;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.CategoriaDtos;


namespace EcommerceAPI.Repository
{
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public CategoriaModel AdicionarCategoria(CategoriaModel categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public IQueryable<CategoriaModel> FiltrarTodos()
        {
            var categorias = _context.Categorias;
            return categorias;            
        }

        public IQueryable<CategoriaModel> FiltrarPorNome(FiltroCategoriaDto filtro)
        {
            var categorias = _context.Categorias.Where(c => c.Nome.ToLower().Contains(filtro.NomeCat.ToLower()));
            return categorias;
        }

        public CategoriaModel VerificarId(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            return categoria;
        }

        public CategoriaModel VerficarNomeExiste(CategoriaModel categoria)
        {
            var nomeExiste = _context.Categorias.FirstOrDefault(c => c.Nome == categoria.Nome);
            return nomeExiste;
        }

        public CategoriaModel EditarNome(UpdateCategoriaDto updateCategoria)
        {
            var editar = _context.Categorias.FirstOrDefault(c => c.Nome == updateCategoria.Nome);
            return editar;
        }

        public IQueryable<SubcategoriaModel> ValidarCategoriaId(int id)
        {
            var subcategoria = _context.Subcategorias.Where(c => c.CategoriaId == id);
            return subcategoria;
        }

        public void DeletarCategoria(CategoriaModel categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
