using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Model;

public class Origem
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [NotNull]
    public string Local { get; set; }
    public int Ativo { get; set; }
    public virtual ICollection<Transicao> Transicoes { get; set; }
}
