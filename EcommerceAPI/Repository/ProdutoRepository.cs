using EcommerceAPI.Data;
using EcommerceAPI.Data.ProdutoDtos;
using System.Linq;
using EcommerceAPI.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EcommerceAPI.Repository
{
    public class ProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProdutoModel> PesquisarPorId(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM Produtos WHERE Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<ProdutoModel>(sql, new { Id = id });
            return result;
        }

        /*public async Task<IReadOnlyList<ProdutoModel>> PesquisarTodos(PaginacaoDto filtros)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM Produtos";
            var result = await connection.QueryAsync<ProdutoModel>(sql);
            return result.Skip(filtros.Pagina * filtros.ItensPagina).Take(filtros.ItensPagina).ToList();
        }*/

        public async Task<List<ProdutoModel>> PesquisarPorFiltros(FiltroProdutoDto filtros, PaginacaoDto paginacao, string ordem)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM Produtos WHERE ";

            if (filtros.NomeProd != null)
                sql += "Nome LIKE \"%" + filtros.NomeProd + "%\" and ";

            if (filtros.DescricaoProd != null)
                sql += "Descricao LIKE \"%" + filtros.DescricaoProd + "%\" and ";

            if (filtros.PesoProd != null)
                sql += "Peso = @peso and ";

            if (filtros.AlturaProd != null)
                sql += "Altura = @altura and ";

            if (filtros.LarguraProd != null)
                sql += "Largura = @largura and ";

            if (filtros.ComprimentoProd != null)
                sql += "Comprimento = @comprimento and ";

            if (filtros.ValorProd != null)
                sql += "Valor = @valor and ";

            if (filtros.QuantidadeProd != null)
                sql += "Quantidade = '@quantidade' and ";

            if (filtros.CentroDistribuicaoProd != null)
                sql += "CentroDistribuicao LIKE \"%" + filtros.CentroDistribuicaoProd + "%\" and ";

            if (filtros.StatusProd != null)
                sql += "Status = @status and ";

            if (filtros.NomeProd == null && filtros.DescricaoProd == null && filtros.PesoProd == null && filtros.AlturaProd == null &&
                filtros.LarguraProd == null && filtros.ComprimentoProd == null && filtros.ValorProd == null && filtros.QuantidadeProd == null &&
                filtros.CentroDistribuicaoProd == null && filtros.StatusProd == null)
            {
                var removerWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(removerWhere);
            }
            else
            {
                var removerAnd = sql.LastIndexOf("and");
                sql = sql.Remove(removerAnd);
            }

            if (ordem == "cat")
                sql += "ORDER BY CategoriaId";

            if (ordem == "sub")
                sql += "ORDER BY SubcategoriaId";

            if (ordem == "nomedesc")
                sql += "ORDER BY Nome DESC";

            if (ordem == "catdesc")
                sql += "ORDER BY CategoriaId DESC";

            if (ordem == "CentroDistribuicaoDesc")
                sql += "ORDER BY CentroDistribuicao DESC";

            var result = await connection.QueryAsync<ProdutoModel>(sql, new
            {
                Nome = filtros.NomeProd,
                Descricao = filtros.DescricaoProd,
                Peso = filtros.PesoProd,
                Altura = filtros.AlturaProd,
                Largura = filtros.LarguraProd,
                Comprimento = filtros.ComprimentoProd,
                Valor = filtros.ValorProd,
                Quantidade = filtros.QuantidadeProd,
                CentroDistribuicao = filtros.CentroDistribuicaoProd,
                Status = filtros.StatusProd,
            });
            return result.Skip(paginacao.Pagina * paginacao.ItensPagina).Take(paginacao.ItensPagina).ToList();
        }
    }
}