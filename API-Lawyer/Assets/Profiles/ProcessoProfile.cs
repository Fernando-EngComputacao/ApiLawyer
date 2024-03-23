using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Model;
using AutoMapper;

namespace API_Lawyer.Assets.Profiles
{
    public class ProcessoProfile : Profile
    {
        public ProcessoProfile()
        {
            CreateMap<CreateProcessoDTO, Processo>();
            CreateMap<UpdateProcessoDTO, Processo>();
            CreateMap<Processo, UpdateProcessoDTO>();
            CreateMap<Processo, ReadProcessoDTO>();
            CreateMap<Processo, CreateProcessoDTO>();
        }
    }
}
