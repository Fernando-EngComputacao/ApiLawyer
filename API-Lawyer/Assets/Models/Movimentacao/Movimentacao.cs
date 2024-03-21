using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Model;
public class Movimentacao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [NotNull]
        public string Descricao { get; set; }
        [Required]
        [NotNull]
        public DateTime Date { get; set; }
        public int Ativo { get; set; }

        public int? ProcessoId { get; set; }
        public virtual Processo Processo { get; set; }



}