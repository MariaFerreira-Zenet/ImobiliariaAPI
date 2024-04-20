using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.DTOs
{
    public class UpdateCorretorDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
