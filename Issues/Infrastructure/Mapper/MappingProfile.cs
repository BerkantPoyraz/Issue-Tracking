using AutoMapper;
using Issues.Models;
using Microsoft.AspNetCore.Identity;

namespace Issues.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDt, IdentityUser>();
            CreateMap<UserForUpdate, IdentityUser>().ReverseMap();
        }
    }
}
