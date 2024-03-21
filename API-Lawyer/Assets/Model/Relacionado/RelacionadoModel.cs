using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Relacionado
{
    public class RelacionadoModel
    {
        [Key]
        [Required]
        private int Id { get; set; }
        private enum Tipo { Apensos, Vinculados }

        private string Descricao { get; set; }
        private DateTime Data { get; set; }
        private string IdNumeroProcesso { get; set; }




    }
}
