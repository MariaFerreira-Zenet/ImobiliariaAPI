using ImobiliariaApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImobiliariaApi.Models
{
    public class ProprietarioCorretor
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Proprietario")]
        public int ProprietarioId { get; set; }
        public Proprietario Proprietario { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Corretor")]
        public int CorretorId { get; set; }
        public Corretor Corretor { get; set; }
    }
}
