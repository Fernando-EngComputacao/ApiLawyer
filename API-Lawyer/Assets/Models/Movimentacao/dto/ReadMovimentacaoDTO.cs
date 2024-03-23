namespace API_Lawyer.Assets.Model.Movimentacao.dto
{
    public class ReadMovimentacaoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Date { get; set; }
        public int Ativo { get; set; }
    }
}
