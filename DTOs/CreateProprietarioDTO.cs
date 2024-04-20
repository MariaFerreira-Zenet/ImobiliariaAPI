using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class CreateProprietarioDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }

        [Required]
        [StringLength(20)]
        [Phone(ErrorMessage = "Formato de telefone inválido")]
        public string Telefone { get; set; }
    }

}
