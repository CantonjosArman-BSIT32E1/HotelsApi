using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, GetStateModel>().ReverseMap();
            CreateMap<State, CreateStateModel>().ReverseMap();
            CreateMap<State, UpdateStateModel>().ReverseMap();
        }
    }
}
