using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Destino
{
    public class DestinoModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private string Local { get; set; }
        private string Nome { get; set; }
        private DateTime DataRecebimento { get; set; }

    }
}
