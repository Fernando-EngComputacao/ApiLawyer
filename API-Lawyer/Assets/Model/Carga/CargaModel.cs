using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Carga
{
    public class CargaModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private string IdOrigem { get; set; }
        private string IdDestino { get; set; }
        private string IdNumeroProcesso { get; set; }


    }
}
