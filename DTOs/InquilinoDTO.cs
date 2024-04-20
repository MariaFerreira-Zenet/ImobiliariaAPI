namespace ImobiliariaApi.DTOs
{
    public class InquilinoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public List<ImovelAlugadoDTO> ImoveisAlugados { get; set; }
    }
}
