using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Movimentacao
{
    public class MovimentacaoModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private string Descricao { get; set; }
        private DateTime Date { get; set; }
        private string IdNumeroProcesso { get; set; }



    }
}
