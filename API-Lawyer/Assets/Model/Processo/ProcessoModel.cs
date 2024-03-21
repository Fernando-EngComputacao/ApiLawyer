using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Processo
{
    public class ProcessoModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private string Numero { get; set; }
        private string Classe { get; set; }
        private string Area { get; set; }
        private string Assunto { get; set; }
        private string Distribuicao { get; set; }
        private string Relator { get; set; }
        private string Volume { get; set; }

    }
}
