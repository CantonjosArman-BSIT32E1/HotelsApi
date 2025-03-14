using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class BarangayProfile : Profile
    {
        public BarangayProfile()
        {
            CreateMap<Barangay, GetBarangayModel>().ReverseMap();
            CreateMap<Barangay, CreateBarangayModel>().ReverseMap();
            CreateMap<Barangay, UpdateBarangayModel>().ReverseMap();
        }
    }
}
