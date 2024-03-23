using API_Lawyer.Model;
using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Model;

    public class Transicao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int? ProcessoId { get; set; }
        public virtual Processo Processo { get; set; }
        public int? OrigemId { get; set; }
        public virtual Origem Origem { get; set; }
        public int Ativo { get; set; }
}

