using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Assets.Models.Movimentacao.dto
{
    public class CreateMovimentacaoDbDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Date { get; set; }
        public int Ativo { get; set; }

        public int? ProcessoId { get; set; }
    }
}
