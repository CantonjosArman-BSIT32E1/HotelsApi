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
    public interface ICityService
    {
        Task<List<GetCityModel>> GetAllCities();
        Task<GetCityModel> GetCityById(int id);
        Task<GetCityModel> CreateCity(CreateCityModel city);
        Task<GetCityModel?> UpdateCity(UpdateCityModel city, int id);
        Task<bool> DeleteCity(int id);
    }

    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public CityService(DatabaseContext databaseContext, ICityRepository cityRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public async Task<GetCityModel> CreateCity(CreateCityModel city)
        {
            CreateCityValidator validator = new CreateCityValidator(databaseContext);
            ValidationResult results = validator.Validate(city);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createCityResult = await cityRepository.CreateCity(mapper.Map<City>(city));
            return mapper.Map<GetCityModel>(createCityResult);
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

        public async Task<List<GetCityModel>> GetAllCities()
        {
            List<City> listOfCities = await databaseContext.City.ToListAsync();
            return mapper.Map<List<GetCityModel>>(listOfCities);
        }

        public async Task<GetCityModel> GetCityById(int id)
        {
            City? city = await databaseContext.City.Where(c => c.CityId == id).FirstOrDefaultAsync();
            return city != null ? mapper.Map<GetCityModel>(city) : null;
        }

        public async Task<GetCityModel?> UpdateCity(UpdateCityModel city, int id)
        {
            var cityFromDatabase = await databaseContext.City.Where(c => c.CityId == id).FirstOrDefaultAsync();

            if (cityFromDatabase == null)
                return null;

            cityFromDatabase.CityCode = city.CityCode;
            cityFromDatabase.CityName = city.CityName;

            await databaseContext.SaveChangesAsync();
            return mapper.Map<GetCityModel>(cityFromDatabase);
        }
    }
}
