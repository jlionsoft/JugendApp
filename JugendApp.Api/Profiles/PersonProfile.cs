using AutoMapper;
using JugendApp.Api.DTOs;
using JugendApp.SharedModels.Person;

namespace JugendApp.Api.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ContactOption, ContactOptionDto>().ReverseMap();
            CreateMap<Instrument, InstrumentDto>().ReverseMap();
            CreateMap<InstrumentSkill, InstrumentSkillDto>().ReverseMap();
        }

    }
}
