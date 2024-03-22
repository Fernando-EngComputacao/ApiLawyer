using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Model;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface IMovimentacaoService
    {
        Task<ReadMovimentacaoDTO> CreateMovimentacaoAsync(CreateMovimentacaoDTO dto);
        Task<IEnumerable<ReadMovimentacaoDTO>> GetAllMovimentacoesAsync(int skip = 0, int take = 10);
        Task<IEnumerable<ReadMovimentacaoDTO>> GetLogicDeletionMovimentacoesAsync(int skip = 0, int take = 10);
        Task<ReadMovimentacaoDTO> GetMovimentacaoByIdAsync(long id);
        Task<Movimentacao> UpdateMovimentacaoAsync(int id, UpdateMovimentacaoDTO dto);
        Task<Movimentacao> DeleteMovimentacaoAsync(int id);
        Task<Movimentacao> LogicalDeleteMovimentacaoAsync(int id);
        Task<int?> GetIdAsync();
    }
}
