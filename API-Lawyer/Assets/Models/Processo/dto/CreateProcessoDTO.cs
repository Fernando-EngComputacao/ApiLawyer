using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Processo.dto
{
    public class CreateProcessoDTO
    {
        [Required]
        public string NumeroProcesso { get; set; }
        [Required]
        public string Classe { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Distribuicao { get; set; }
        [Required]
        public string Relator { get; set; }
        [Required]
        public string Volume { get; set; }
    }
}
