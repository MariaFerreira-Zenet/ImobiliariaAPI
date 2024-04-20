namespace ImobiliariaApi.DTOs
{
    public class ProprietarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public List<ImovelDTO> Imoveis { get; set; }
        public List<SimpleCorretorDTO> Corretores { get; set; }
    }
}
