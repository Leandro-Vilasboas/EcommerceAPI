using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SubcategoriaModel>()
                .HasOne(subcategoria => subcategoria.Categoria)
                .WithMany(categoria => categoria.Subcategoria)
                .HasForeignKey(subcategoria => subcategoria.CategoriaId);

            builder.Entity<ProdutoModel>()
                .HasOne(produto => produto.Categoria)
                .WithMany(categoria => categoria.Produto)
                .HasForeignKey(produto => produto.CategoriaId);

            builder.Entity<ProdutoModel>()
                .HasOne(produto => produto.Subcategoria)
                .WithMany(subcategoria => subcategoria.Produto)
                .HasForeignKey(produto => produto.SubcategoriaId);

            builder.Entity<ProdutoModel>()
                .HasOne(produto => produto.CentroDeDistribuicao)
                .WithMany(centroDeDistribuicao => centroDeDistribuicao.Produto)
                .HasForeignKey(produto => produto.CentroDeDistribuicaoId);
        }

        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<SubcategoriaModel> Subcategorias { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CDModel> CentroDeDistribuicao { get; set; }
    }
}
