using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos
{
    public class CreateSubcategoriaDto
    {
        [Required(ErrorMessage = "O nome da subcategoria é obrigatório")]
        [StringLength(128, ErrorMessage = "O nome da subcategoria deve ter no máximo 128 caracteres")]
        [RegularExpression("^[a-zA-Z\u00C0-\u00FF ]*$", ErrorMessage = "O nome da subcategoria deve conter apenas letras")]
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
    }
}
