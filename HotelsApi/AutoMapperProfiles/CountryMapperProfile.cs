using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, GetCountryModel>().ReverseMap();
            CreateMap<Country, CreateCountryModel>().ReverseMap();
            CreateMap<Country, UpdateCountryModel>().ReverseMap();
        }
    }
}
