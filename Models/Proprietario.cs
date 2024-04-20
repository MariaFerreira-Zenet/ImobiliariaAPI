using ImobiliariaApi.Models;
using System.ComponentModel.DataAnnotations;

public class Proprietario
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [StringLength(11)]
    public string CPF { get; set; }

    [Required]
    [StringLength(20)]
    public string Telefone { get; set; }

    public virtual ICollection<Imovel> Imoveis { get; set; }
    public virtual ICollection<ProprietarioCorretor> Corretores { get; set; }

    public Proprietario()
    {
        Imoveis = new HashSet<Imovel>();
        Corretores = new HashSet<ProprietarioCorretor>();
    }
}
