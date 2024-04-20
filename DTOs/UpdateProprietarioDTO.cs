using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class UpdateProprietarioDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(20)]
        [Phone(ErrorMessage = "Formato de telefone inválido")]
        public string Telefone { get; set; }
    }

}
