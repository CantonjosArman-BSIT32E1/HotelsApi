using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountries();
        Task<Country> GetCountryById(int id);
        Task<Country> CreateCountry(Country country);
        Task<Country?> UpdateCountry(Country country, int id);
        Task<bool> DeleteCountry(int id);
    }

    public class CountryRepository : ICountryRepository
    {
        public DatabaseContext databaseContext;

        public CountryRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Country> CreateCountry(Country country)
        {
            await databaseContext.Country.AddAsync(country);
            await databaseContext.SaveChangesAsync();
            return country;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            var countryFromDatabase = await databaseContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();

            if (countryFromDatabase == null)
                return false;

            databaseContext.Remove(countryFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            List<Country> listOfCountries = await databaseContext.Country.ToListAsync();
            return listOfCountries;
        }

        public async Task<Country> GetCountryById(int id)
        {
            Country? country = await databaseContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();

            if (country != null)
                return country;

            return null;
        }

        public async Task<Country?> UpdateCountry(Country country, int id)
        {
            var countryFromDatabase = await databaseContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();

            if (countryFromDatabase == null)
                return null;

            countryFromDatabase.CountryCode = country.CountryCode;
            countryFromDatabase.CountryName = country.CountryName;

            await databaseContext.SaveChangesAsync();
            return countryFromDatabase;
        }
    }
}