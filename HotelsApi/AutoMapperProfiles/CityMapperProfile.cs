using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, GetCityModel>().ReverseMap();
            CreateMap<City, CreateCityModel>().ReverseMap();
            CreateMap<City, UpdateCityModel>().ReverseMap();
        }
    }
}
