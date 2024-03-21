using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Model;

    public class Processo
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [NotNull]
        public string NumeroProcesso { get; set; }
        [Required]
        [NotNull]
        public string Classe { get; set; }
        [Required]
        [NotNull]
        public string Area { get; set; }
        [Required]
        [NotNull]
        public string Assunto { get; set; }
        [Required]
        [NotNull]
        public string Distribuicao { get; set; }
        [Required]
        [NotNull]
        public string Relator { get; set; }
        [Required]
        public string Volume { get; set; }
        public int Ativo { get; set; }
        public virtual ICollection<Transicao> Transicoes { get; set; }
        public virtual ICollection<Movimentacao> Movimentacoes { get; set; }


}
