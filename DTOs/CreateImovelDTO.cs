using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class CreateImovelDTO
    {
        [Required]
        public string Endereco { get; set; }

        [Required]
        public int ProprietarioId { get; set; }
        public int? InquilinoId { get; set; }
    }
}
