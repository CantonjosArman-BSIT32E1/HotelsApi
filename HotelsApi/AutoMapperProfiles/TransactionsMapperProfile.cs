using AutoMapper;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.AutoMapperProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, GetTransactionModel>().ReverseMap();
            CreateMap<Transaction, CreateTransactionModel>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionModel>().ReverseMap();
        }
    }
}
