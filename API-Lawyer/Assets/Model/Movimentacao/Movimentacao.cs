using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Assets.Model.Movimentacao
{
    public class Movimentacao
    {
        [Key]
        [Required]
        private int Id { get; set; }
        [Required]
        [NotNull]
        private string Descricao { get; set; }
        [Required]
        [NotNull]
        private DateTime Date { get; set; }



    }
}
