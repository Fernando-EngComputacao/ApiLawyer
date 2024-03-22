using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Assets.Services.Interfaces
{
    public interface IOrigemService
    {
        Task<ReadOrigemDTO> CreateOrigemAsync(CreateOrigemDTO dto);
        Task<IEnumerable<ReadOrigemDTO>> GetAllOrigensAsync(int skip = 0, int take = 10);
        Task<IEnumerable<ReadOrigemDTO>> GetLogicDeletionOrigensAsync(int skip = 0, int take = 10);
        Task<ReadOrigemDTO> GetOrigemByIdAsync(long id);
        Task<Origem> UpdateOrigemAsync(int id, UpdateOrigemDTO dto);
        Task<Origem> DeleteOrigemAsync(int id);
        Task<Origem> LogicalDeleteOrigemAsync(int id);
        Task<int?> GetOrigemByLocalAsync(string local);
        Task<int?> GetIdAsync();
    }
}

