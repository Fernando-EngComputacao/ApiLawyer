using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Model;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface ITransicaoService
    {
        Task<ReadTransicaoDTO> CreateTransicaoAsync(CreateTransicaoDTO dto);
        Task<IEnumerable<ReadTransicaoDTO>> GetAllTransicaoAsync(int skip = 0, int take = 10);
        Task<ReadTransicaoDTO> GetTransicaoByIdAsync(long id);
        Task<Transicao> UpdateTransicaoAsync(int id, UpdateTransicaoDTO dto);
        Task<Transicao> DeleteTransicaoAsync(int id);
        Task<Transicao> LogicalDeleteTransicaoAsync(int id);
    }
}
