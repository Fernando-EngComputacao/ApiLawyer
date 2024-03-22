using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Lawyer.Assets.Models.Origem.dto
{
    public class CreateOrigemDbDTO
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public int Ativo { get; set; }
    }
}
