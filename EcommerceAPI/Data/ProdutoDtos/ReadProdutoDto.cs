using EcommerceAPI.Models;
using System;
using System.Collections.Generic;

namespace EcommerceAPI.Data.ProdutoDtos
{
    public class ReadProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string CentroDeDistribuicao { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public DateTime? DataDeAlteracao { get; set; } = DateTime.Now;
        public static CategoriaModel Categoria { get; set; }
        public static SubcategoriaModel Subcategoria { get; set; }
    }
}
