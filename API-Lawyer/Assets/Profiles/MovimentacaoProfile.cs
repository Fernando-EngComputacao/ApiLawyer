using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Model;
using AutoMapper;

namespace API_Lawyer.Assets.Profiles
{
    public class MovimentacaoProfile : Profile
    {
        public MovimentacaoProfile() 
        {
            CreateMap<CreateMovimentacaoDTO, Movimentacao>();
            CreateMap<UpdateMovimentacaoDTO, Movimentacao>();
            CreateMap<Movimentacao, UpdateMovimentacaoDTO>();
            CreateMap<Movimentacao, ReadMovimentacaoDTO>();
        }
    }
}
