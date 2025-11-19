using AutoMapper;
using JugendApp.DTOs;
using JugendApp.SharedModels.Events;

namespace JugendApp.Api.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Invitation,  InvitationDto>().ReverseMap();
        }

    }
}
