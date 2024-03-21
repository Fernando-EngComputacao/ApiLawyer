using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Origem
{
    public class OrigemModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private string Local { get; set; }
        private string Nome { get; set; }
        private DateTime DataRemessa { get; set; }




    }
}
