using AutoMapper;
using Span.Culturio.Api.Data.Entity;
using Span.Culturio.Api.Models;

namespace Span.Culturio.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, UserDto>();
            CreateMap<UserDto, RegisterUserDto>();
        }
    }
}
