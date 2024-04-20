using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class UpdateInquilinoDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }

        [StringLength(20)]
        [Phone(ErrorMessage = "Formato de telefone inválido")]
        public string Telefone { get; set; }

    }

}
