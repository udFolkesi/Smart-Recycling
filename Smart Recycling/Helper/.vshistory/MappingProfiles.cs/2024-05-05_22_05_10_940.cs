using AutoMapper;
using CORE.Models;
using Smart_Recycling.Dto;

namespace Smart_Recycling.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
