using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImobiliariaApi.Models
{
    public class CorretorInquilino
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Corretor")]
        public int CorretorId { get; set; }
        public Corretor Corretor { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Inquilino")]
        public int InquilinoId { get; set; }
        public Inquilino Inquilino { get; set; }
    }
}
