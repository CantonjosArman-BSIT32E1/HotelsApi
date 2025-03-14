using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface IHotelRepository
    {
        Task<List<Hotels>> GetAllHotels();
        Task<Hotels> GetHotelById(int id);
        Task<Hotels> CreateHotel(Hotels hotel);
        Task<Hotels?> UpdateHotel(Hotels hotel, int id);
        Task<bool> DeleteHotel(int id);
    }

    public class HotelRepository : IHotelRepository
    {
        public DatabaseContext databaseContext;

        public HotelRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Hotels> CreateHotel(Hotels hotel)
        {
            await databaseContext.Hotels.AddAsync(hotel);
            await databaseContext.SaveChangesAsync();
            return hotel;
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

        public async Task<List<Hotels>> GetAllHotels()
        {
            List<Hotels> listOfHotels = await databaseContext.Hotels.ToListAsync();
            return listOfHotels;
        }

        public async Task<Hotels> GetHotelById(int id)
        {
            Hotels? hotel = await databaseContext.Hotels.Where(h => h.HotelId == id).FirstOrDefaultAsync();

            if (hotel != null)
                return hotel;

            return null;
        }

        public async Task<Hotels?> UpdateHotel(Hotels hotel, int id)
        {
            var hotelFromDatabase = await databaseContext.Hotels.Where(h => h.HotelId == id).FirstOrDefaultAsync();

            if (hotelFromDatabase == null)
                return null;

            hotelFromDatabase.HotelCode = hotel.HotelCode;
            hotelFromDatabase.HotelName = hotel.HotelName;
            hotelFromDatabase.HotelDescription = hotel.HotelDescription;


            await databaseContext.SaveChangesAsync();
            return hotelFromDatabase;
        }
    }
}