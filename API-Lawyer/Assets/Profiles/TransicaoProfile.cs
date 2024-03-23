using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Model;
using AutoMapper;

namespace API_Lawyer.Assets.Profiles
{
    public class TransicaoProfile : Profile
    {
        public TransicaoProfile()
        {
            CreateMap<CreateTransicaoDTO, Transicao>();
            CreateMap<UpdateTransicaoDTO, Transicao>();
            CreateMap<Transicao, UpdateTransicaoDTO>();
            CreateMap<Transicao, ReadTransicaoDTO>();
            CreateMap<Transicao, CreateTransicaoDTO>();
        }
    }
}
