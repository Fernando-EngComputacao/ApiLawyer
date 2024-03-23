using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Model;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface IProcessoService
    {
        Task<ReadProcessoDTO> CreateProcessoAsync(CreateProcessoDTO dto);
        Task<IEnumerable<ReadProcessoDTO>> GetAllProcessosAsync(int skip = 0, int take = 10);
        Task<IEnumerable<ReadProcessoDTO>> GetLogicDeletionProcessosAsync(int skip = 0, int take = 10);
        Task<ReadProcessoDTO> GetProcessoByIdAsync(long id);
        Task<Processo> UpdateProcessoAsync(int id, UpdateProcessoDTO dto);
        Task<Processo> DeleteProcessoAsync(int id);
        Task<Processo> LogicalDeleteProcessoAsync(int id);
        Task<ReadProcessoDTO> GetProcessoByNumeroProcessoAsync(string numeroProcesso);
        Task<int?> GetIdAsync();
    }
}
