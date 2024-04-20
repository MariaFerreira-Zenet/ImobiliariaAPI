using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.Models
{
    public class Imovel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Endereco { get; set; }

        [ForeignKey("Proprietario")]
        public int ProprietarioId { get; set; }
        public Proprietario Proprietario { get; set; }


        [ForeignKey("Inquilino")]
        public int? InquilinoId { get; set; }
        public Inquilino Inquilino { get; set; }
    }
}
