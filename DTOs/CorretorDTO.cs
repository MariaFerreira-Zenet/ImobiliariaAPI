namespace ImobiliariaApi.DTOs
{
    public class CorretorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<SimpleProprietarioDTO> Proprietarios { get; set; }
    }
}
