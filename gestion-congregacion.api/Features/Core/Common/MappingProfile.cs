using AutoMapper;
using gestion_congregacion.api.Features.Users;
using gestion_congregacion.api.Features.Users.DTO;

namespace gestion_congregacion.api.Features.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FullName, x => x
                // https://github.com/dotnet/efcore/issues/28894
                .MapFrom(src => src.FirstName +" "+src.LastName));

        }
    }
}
