using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using HotelsApi.Repositories;
using HotelsApi.Validators;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Services
{
    public interface IHotelService
    {
        Task<List<GetHotelModel>> GetAllHotels();
        Task<GetHotelModel> GetHotelById(int id);
        Task<GetHotelModel> CreateHotel(CreateHotelModel hotel);
        Task<GetHotelModel?> UpdateHotel(UpdateHotelModel hotel, int id);
        Task<bool> DeleteHotel(int id);
    }

    public class HotelService : IHotelService
    {
        private readonly IHotelRepository hotelRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public HotelService(DatabaseContext databaseContext, IHotelRepository hotelRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        public async Task<GetHotelModel> CreateHotel(CreateHotelModel hotel)
        {
            CreateHotelValidator validator = new CreateHotelValidator(databaseContext);
            ValidationResult results = validator.Validate(hotel);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createHotelResult = await hotelRepository.CreateHotel(mapper.Map<Hotels>(hotel));
            return mapper.Map<GetHotelModel>(createHotelResult);
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var hotelFromDatabase = await databaseContext.Hotels.Where(h => h.HotelId == id).FirstOrDefaultAsync();
            if (hotelFromDatabase == null)
                return false;

            databaseContext.Remove(hotelFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetHotelModel>> GetAllHotels()
        {
            List<Hotels> listOfHotels = await databaseContext.Hotels.ToListAsync();
            return mapper.Map<List<GetHotelModel>>(listOfHotels);
        }

        public async Task<GetHotelModel> GetHotelById(int id)
        {
            Hotels? hotel = await databaseContext.Hotels.Where(h => h.HotelId == id).FirstOrDefaultAsync();
            return hotel != null ? mapper.Map<GetHotelModel>(hotel) : null;
        }

        public async Task<GetHotelModel?> UpdateHotel(UpdateHotelModel hotel, int id)
        {
            var hotelFromDatabase = await databaseContext.Hotels.Where(h => h.HotelId == id).FirstOrDefaultAsync();
            if (hotelFromDatabase == null)
                return null;

            hotelFromDatabase.HotelCode = hotel.HotelCode;
            hotelFromDatabase.HotelName = hotel.HotelName;
            hotelFromDatabase.HotelDescription = hotel.HotelDescription;

            await databaseContext.SaveChangesAsync();
            return mapper.Map<GetHotelModel>(hotelFromDatabase);
        }
    }
}
