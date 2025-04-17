using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos
{
    public class UpdateCategoriaDto
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome da categoria deve ter no máximo 128 caracteres")]
        [RegularExpression("^[a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O nome da categoria deve conter apenas letras")]
        public string Nome { get; set; }
    }
}
