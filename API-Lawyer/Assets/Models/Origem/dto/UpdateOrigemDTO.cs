using System.ComponentModel.DataAnnotations;

namespace API_Lawyer.Assets.Model.Origem.dto
{
    public class UpdateOrigemDTO
    {
        [Required]
        public string Local { get; set; }
    }
}
