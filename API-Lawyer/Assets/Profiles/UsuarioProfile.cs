using API_Lawyer.Assets.Models.Usuario;
using API_Lawyer.Assets.Models.Usuario.dto;
using AutoMapper;

namespace API_Lawyer.Assets.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDTO, Usuario>();
            CreateMap<Usuario, CreateUsuarioDTO>();
            CreateMap<Usuario, ReadUsuarioDTO>();
            CreateMap<ReadUsuarioDTO, Usuario>();
        }
    }
}
