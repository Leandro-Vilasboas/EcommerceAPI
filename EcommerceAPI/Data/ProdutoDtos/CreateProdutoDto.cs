using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.ProdutoDtos
{
    public class CreateProdutoDto
    {
        public string Nome { get; set; }
        [Required(ErrorMessage = "A descrição do produto é obrigatório")]
        [StringLength(128, ErrorMessage = "A descrição do produto deve ter no máximo 512 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "A descrição do produto deve conter apenas letras e números")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "É necessário informar o peso do produto")]
        public double Peso { get; set; }
        [Required(ErrorMessage = "É necessário informar a altura do produto")]
        public double Altura { get; set; }
        [Required(ErrorMessage = "É necessário informar a largura do produto")]
        public double Largura { get; set; }
        [Required(ErrorMessage = "É necessário informar o comprimento do produto")]
        public double Comprimento { get; set; }
        [Required(ErrorMessage = "É necessário informar o valor do produto")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "É necessário informar a quantidade do produto em estoque")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "É necessário informar o ID da categoria")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "É necessário informar o ID da subcategoria")]
        public int SubcategoriaId { get; set; }
        public int CentroDeDistribuicaoId { get; set; }
    }
}
