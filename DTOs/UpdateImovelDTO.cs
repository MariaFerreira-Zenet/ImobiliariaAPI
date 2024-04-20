using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class UpdateImovelDTO
    {
        [Required]
        public string Endereco { get; set; }

        public int ProprietarioId { get; set; }

        public int? InquilinoId { get; set; }
    }

}
