using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Peticao
{
    public class PeticaoModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private enum Tipo { Petição }
        private DateTime Data { get; set; }
        private string IdNumeroProcesso { get; set; }



    }
}
