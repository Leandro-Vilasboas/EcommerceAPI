using Microsoft.EntityFrameworkCore;
using System.Linq;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.CategoriaDtos;
using System.Threading.Tasks;
using EcommerceAPI.Interfaces.Reposity;


namespace EcommerceAPI.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriaModel> AdicionarCategoria(CategoriaModel categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
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

        public async Task<CategoriaModel> VerificarId(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            return categoria;
        }

        public async Task<CategoriaModel> VerficarNomeExiste(CategoriaModel categoria)
        {
            var nomeExiste = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == categoria.Nome);
            return nomeExiste;
        }

        public async Task<CategoriaModel> EditarNome(UpdateCategoriaDto updateCategoria)
        {
            var editar = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == updateCategoria.Nome);
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
