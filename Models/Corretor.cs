using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.Models
{
    public class Corretor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // Asume que o nome tem um máximo de 100 caracteres.
        public string Nome { get; set; }

        // Relacionamento muitos-para-muitos com Proprietário
        public virtual ICollection<ProprietarioCorretor> Proprietarios { get; set; }

        // Relacionamento muitos-para-muitos com Inquilino
        public virtual ICollection<CorretorInquilino> Inquilinos { get; set; }


        public Corretor()
        {
            Proprietarios = new HashSet<ProprietarioCorretor>();
            Inquilinos = new HashSet<CorretorInquilino>();
        }
    }
}
