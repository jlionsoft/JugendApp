using AutoMapper;
using JugendApp.Api.DTOs;
using JugendApp.SharedModels.Events;

namespace JugendApp.Api.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<SimpleEvent, SimpleEventDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}
