using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.SubcategoriaDtos;
using EcommerceAPI.Interfaces.Repository;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Repository
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {

        private readonly AppDbContext _context;

        public SubcategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SubcategoriaModel> AdicionarSubcategoria(SubcategoriaModel subcategoria)
        {
            await _context.Subcategorias.AddAsync(subcategoria);
            _context.SaveChanges();
            return subcategoria;
        }

        public async Task<CategoriaModel> ValidarCategoriaId(CreateSubcategoriaDto subcategoriaDto)
        {
            var categoriaId = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == subcategoriaDto.CategoriaId);
            return categoriaId;
        }
        
        public async Task<SubcategoriaModel> VerificarNomeExiste(CreateSubcategoriaDto subcategoriaDto)
        {
            var nomeExistente = await _context.Subcategorias.FirstOrDefaultAsync(c => c.Nome == subcategoriaDto.Nome);
            return nomeExistente;
        }

        public async Task<SubcategoriaModel> VerificarId(int id)
        {
            var subcategoria = await _context.Subcategorias.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<SubcategoriaModel> EditarNome(UpdateSubcategoriaDto updateSubcategoria)
        {
            var editar = await _context.Subcategorias.FirstOrDefaultAsync(c => c.Nome == updateSubcategoria.Nome);
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
