using AutoMapper;
using DataAccess.DTOs;
using DataAccess.Models;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserTokenDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserEditDto, User>().ForMember(x => x.Photo, opt => opt.Ignore());
        }
    }
}
