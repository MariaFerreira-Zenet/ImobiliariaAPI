using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImobiliariaApi.Models
{
    public class Inquilino
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // Assumindo que o nome tem um máximo de 100 caracteres.
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefone { get; set; }

        // Relacionamento um-para-muitos com Imóvel (um inquilino pode alugar vários imóveis)
        public virtual ICollection<Imovel> ImoveisAlugados { get; set; }

        // Relacionamento muitos-para-muitos com Corretor
        public virtual ICollection<CorretorInquilino> Corretores { get; set; }

        // Construtor para inicializar as listas de relacionamentos
        public Inquilino()
        {
            ImoveisAlugados = new HashSet<Imovel>();
            Corretores = new HashSet<CorretorInquilino>();
        }
    }
}
