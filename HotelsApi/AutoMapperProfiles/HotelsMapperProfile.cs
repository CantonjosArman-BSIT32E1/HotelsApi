using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotels, GetHotelModel>().ReverseMap();
            CreateMap<Hotels, CreateHotelModel>().ReverseMap();
            CreateMap<Hotels, UpdateHotelModel>().ReverseMap();
        }
    }
}