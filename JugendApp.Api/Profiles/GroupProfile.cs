using AutoMapper;
using JugendApp.DTOs;
using JugendApp.SharedModels.Groups;

namespace JugendApp.Api.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupMember, GroupMemberDto>().ReverseMap();
        }

    }
}
