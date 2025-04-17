using EcommerceAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.CDDtos
{
    public class CreateCDDto
    {
        public string CEP { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome do Centro de Distribuição deve ter no máximo 128 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O nome do Centro de Distribuição deve ser alfanumérico")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O logradouro é obrigatório")]
        [StringLength(256, ErrorMessage = "O nome logradouro deve ter no máximo 256 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O logradouro do Centro de Distribuição deve ser alfanumérico")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O número do Centro de Distribuição é obrigatório")]
        public int Numero { get; set; }
        [StringLength(128, ErrorMessage = "O complemento do Centro de Distribuição deve ter no máximo 128 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O complemento do Centro de Distribuição deve ser alfanumérico")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O bairro é obrigatório")]
        [StringLength(128, ErrorMessage = "O bairro do Centro de Distribuição deve ter no máximo 128 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O bairro do Centro de Distribuição deve ser alfanumérico")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "A cidade é obrigatório")]
        [StringLength(128, ErrorMessage = "A cidade do Centro de Distribuição deve ter no máximo 128 caracteres")]
        [RegularExpression("^[0-9a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "A cidade do Centro de Distribuição deve ser alfanumérico")]
        public string Localidade { get; set; }
        [ValidarUF(ErrorMessage = "Estádo inválido, insira uma UF brasileira.")]
        public string UF { get; set; }
        public int ProdutoId { get; set; }
    }
}
