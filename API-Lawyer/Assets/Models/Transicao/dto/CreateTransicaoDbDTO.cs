using API_Lawyer.Model;

namespace API_Lawyer.Assets.Models.Transicao.dto
{
    public class CreateTransicaoDbDTO
    {
        public int Id { get; set; }
        public int? ProcessoId { get; set; }
        public int? OrigemId { get; set; }
    }
}
