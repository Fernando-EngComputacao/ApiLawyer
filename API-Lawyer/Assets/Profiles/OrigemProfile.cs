using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Model;
using AutoMapper;

namespace API_Lawyer.Assets.Profiles
{
    public class OrigemProfile : Profile
    {
        public OrigemProfile() 
        {
            CreateMap<CreateOrigemDTO, Origem>();
            CreateMap<UpdateOrigemDTO, Origem>();
            CreateMap<Origem, UpdateOrigemDTO>();
            CreateMap<Origem, ReadOrigemDTO>();
        }
    }
}
