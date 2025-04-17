using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Models
{
    public class SubcategoriaModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome da categoria deve ter no máximo 128 caracteres")]
        [RegularExpression("^[a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O nome da categoria deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataDeCriacao { get; set; } = DateTime.Now;
        public DateTime? DataDeAlteração { get; set; }
        public virtual CategoriaModel Categoria { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public virtual List<ProdutoModel> Produto { get; set; }
    }
}