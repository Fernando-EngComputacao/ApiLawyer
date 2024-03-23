using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Origem.dto
{
    public class CreateOrigemDTO
    {
        [Required]
        public string Local { get; set; }
    }
}
