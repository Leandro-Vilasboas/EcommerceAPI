using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Models
{
    public class CDModel
    {
        [Key]
        [Required]
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataDeCriacao { get; set; } = DateTime.Now;
        public DateTime? DataDeAlteracao { get; set; }

        [JsonIgnore]
        public virtual List<ProdutoModel> Produto { get; set; }
    }
}
