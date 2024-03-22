
namespace API_Lawyer.Assets.Model.Processo.dto
{
    public class CreateProcessoDbDTO
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public string Classe { get; set; }
        public string Area { get; set; }
        public string Assunto { get; set; }
        public string Distribuicao { get; set; }
        public string Relator { get; set; }
        public string Volume { get; set; }
        public int Ativo { get; set; }
    }
}
