using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllCities();
        Task<City> GetCityById(int id);
        Task<City> CreateCity(City city);
        Task<City?> UpdateCity(City city, int id);
        Task<bool> DeleteCity(int id);
    }

    public class CityRepository : ICityRepository
    {
        public DatabaseContext databaseContext;

        public CityRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<City> CreateCity(City city)
        {
            await databaseContext.City.AddAsync(city);
            await databaseContext.SaveChangesAsync();
            return city;
        }

        public async Task<bool> DeleteCity(int id)
        {
            var cityFromDatabase = await databaseContext.City.Where(c => c.CityId == id).FirstOrDefaultAsync();

            if (cityFromDatabase == null)
                return false;

            databaseContext.Remove(cityFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<City>> GetAllCities()
        {
            List<City> listOfCities = await databaseContext.City.ToListAsync();
            return listOfCities;
        }

        public async Task<City> GetCityById(int id)
        {
            City? city = await databaseContext.City.Where(c => c.CityId == id).FirstOrDefaultAsync();

            if (city != null)
                return city;

            return null;
        }

        public async Task<City?> UpdateCity(City city, int id)
        {
            var cityFromDatabase = await databaseContext.City.Where(c => c.CityId == id).FirstOrDefaultAsync();

            if (cityFromDatabase == null)
                return null;

            cityFromDatabase.CityCode = city.CityCode;
            cityFromDatabase.CityName = city.CityName;


            await databaseContext.SaveChangesAsync();
            return cityFromDatabase;
        }
    }
}