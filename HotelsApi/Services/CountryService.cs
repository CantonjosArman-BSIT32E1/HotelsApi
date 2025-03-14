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
    public interface ICountryService
    {
        Task<List<GetCountryModel>> GetAllCountries();
        Task<GetCountryModel> GetCountryById(int id);
        Task<GetCountryModel> CreateCountry(CreateCountryModel country);
        Task<GetCountryModel?> UpdateCountry(UpdateCountryModel country, int id);
        Task<bool> DeleteCountry(int id);
    }

    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public CountryService(DatabaseContext databaseContext, ICountryRepository countryRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public async Task<GetCountryModel> CreateCountry(CreateCountryModel country)
        {
            CreateCountryValidator validator = new CreateCountryValidator(databaseContext);
            ValidationResult results = validator.Validate(country);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createCountryResult = await countryRepository.CreateCountry(mapper.Map<Country>(country));
            return mapper.Map<GetCountryModel>(createCountryResult);
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

        public async Task<List<GetCountryModel>> GetAllCountries()
        {
            List<Country> listOfCountries = await databaseContext.Country.ToListAsync();
            return mapper.Map<List<GetCountryModel>>(listOfCountries);
        }

        public async Task<GetCountryModel> GetCountryById(int id)
        {
            Country? country = await databaseContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();
            if (country != null)
                return mapper.Map<GetCountryModel>(country);

            return null;
        }

        public async Task<GetCountryModel?> UpdateCountry(UpdateCountryModel country, int id)
        {
            var countryFromDatabase = await databaseContext.Country.Where(c => c.CountryId == id).FirstOrDefaultAsync();
            if (countryFromDatabase == null)
                return null;

            countryFromDatabase.CountryCode = country.CountryCode;
            countryFromDatabase.CountryName = country.CountryName;

            await databaseContext.SaveChangesAsync();
            return mapper.Map<GetCountryModel>(countryFromDatabase);
        }
    }
}
