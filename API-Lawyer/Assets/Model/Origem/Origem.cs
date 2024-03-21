using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Assets.Model.Origem
{
    public class Origem
    {
        [Key]
        [Required]
        private int Id { get; set; }
        [Required]
        [NotNull]
        private string Local { get; set; }

    }
}
