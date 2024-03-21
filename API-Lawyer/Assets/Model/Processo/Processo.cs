using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Assets.Model.Processo
{
    public class Processo
    {
        [Key]
        [Required]
        private int Id { get; set; }
        [Required]
        [NotNull]
        private string Numero { get; set; }
        [Required]
        [NotNull]
        private string Classe { get; set; }
        [Required]
        [NotNull]
        private string Area { get; set; }
        [Required]
        [NotNull]
        private string Assunto { get; set; }
        [Required]
        [NotNull]
        private string Distribuicao { get; set; }
        [Required]
        [NotNull]
        private string Relator { get; set; }
        [Required]
        private string Volume { get; set; }

    }
}
