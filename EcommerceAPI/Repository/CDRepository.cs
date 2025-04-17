using EcommerceAPI.Data;
using EcommerceAPI.Data.CDDtos;
using System.Linq;
using EcommerceAPI.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EcommerceAPI.Repository
{
    public class CDRepository
    {
        private readonly IConfiguration _configuration;

        public CDRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CDModel> PesquisarPorId(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM CentroDeDistribuicao WHERE Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<CDModel>(sql, new { Id = id });
            return result;
        }

        /*public async Task<IReadOnlyList<CDModel>> PesquisarTodos(PaginacaoDto filtros)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM CentroDeDistribuicao";
            var result = await connection.QueryAsync<CDModel>(sql);
            return result.Skip(filtros.Pagina * filtros.ItensPagina).Take(filtros.ItensPagina).ToList();
        }*/

        public async Task<List<CDModel>> PesquisarPorFiltros(FiltroCDDto filtros)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("LojaConnection"));
            connection.Open();
            var sql = "SELECT * FROM CentroDeDistribuicao WHERE ";

            if (filtros.NomeCD != null)
                sql += "Nome LIKE \"%" + filtros.NomeCD + "%\" and ";

            if (filtros.LogradouroCD != null)
                sql += "Logradouro LIKE \"%" + filtros.LogradouroCD + "%\" and ";

            if (filtros.NumeroCD != null)
                sql += "Numero = @numero and ";

            if (filtros.ComplementoCD != null)
                sql += "Complemento LIKE \"%" + filtros.ComplementoCD + "%\" and ";

            if (filtros.BairroCD != null)
                sql += "Bairro LIKE \"%" + filtros.BairroCD + "%\" and ";

            if (filtros.CEP != null)
                sql += "CEP = @cep and ";

            if (filtros.LocalidadeCD != null)
                sql += "Localidade LIKE \"%" + filtros.LocalidadeCD + "%\" and ";

            if (filtros.UF != null)
                sql += "UF = '@uf' and ";

            if (filtros.StatusCD != null)
                sql += "Status = @status and ";

            if (filtros.NomeCD == null && filtros.LogradouroCD == null && filtros.NumeroCD == null && filtros.ComplementoCD == null &&
                filtros.BairroCD == null && filtros.CEP == null && filtros.LocalidadeCD == null && filtros.UF == null && filtros.StatusCD == null)
            {
                var removerWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(removerWhere);
            }
            else
            {
                var removerAnd = sql.LastIndexOf("and");
                sql = sql.Remove(removerAnd);
            }

            if (filtros.Ordem == "nomedesc")
                sql += "ORDER BY Nome DESC";

            if (filtros.Ordem == "CentroDistribuicaoDesc")
                sql += "ORDER BY CentroDistribuicao DESC";

            var result = await connection.QueryAsync<CDModel>(sql, new
            {
                Nome = filtros.NomeCD,
                Logradouro = filtros.LogradouroCD,
                Numero = filtros.NumeroCD,
                Complemento = filtros.ComplementoCD,
                Bairro = filtros.BairroCD,
                CEP = filtros.CEP,
                Localidade = filtros.LocalidadeCD,
                UF = filtros.UF,
                Status = filtros.StatusCD,
            });
            return result.Skip(filtros.Pagina * filtros.ItensPagina).Take(filtros.ItensPagina).ToList();
        }
    }
}
