using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Movimentacao.dto
{
    public class CreateMovimentacaoDTO
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int? ProcessoId { get; set; }
    }
}
