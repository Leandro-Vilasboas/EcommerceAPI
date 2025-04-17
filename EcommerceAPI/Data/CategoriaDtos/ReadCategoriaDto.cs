using EcommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public DateTime? DataDeAlteracao { get; set; } = DateTime.Now;
        public static List<SubcategoriaModel> Subcategoria { get; set; }
        public static List<ProdutoModel> Produto { get; set; }
    }
}
