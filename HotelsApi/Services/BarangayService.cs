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
    public interface IBarangayService
    {
        Task<List<GetBarangayModel>> GetAllBarangays();
        Task<GetBarangayModel> GetBarangayById(int id);
        Task<GetBarangayModel> CreateBarangay(CreateBarangayModel barangay);
        Task<GetBarangayModel?> UpdateBarangay(UpdateBarangayModel barangay, int id);
        Task<bool> DeleteBarangay(int id);
    }

    public class BarangayService : IBarangayService
    {
        private readonly IBarangayRepository barangayRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public BarangayService(DatabaseContext databaseContext, IBarangayRepository barangayRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.barangayRepository = barangayRepository;
            this.mapper = mapper;
        }

        public async Task<GetBarangayModel> CreateBarangay(CreateBarangayModel barangay)
        {
            CreateBarangayValidator validator = new CreateBarangayValidator(databaseContext);
            ValidationResult results = validator.Validate(barangay);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createBarangayResult = await barangayRepository.CreateBarangay(mapper.Map<Barangay>(barangay));
            return mapper.Map<GetBarangayModel>(createBarangayResult);
        }

        public async Task<bool> DeleteBarangay(int id)
        {
            var barangayFromDatabase = await databaseContext.Barangay.Where(b => b.BarangayId == id).FirstOrDefaultAsync();
            if (barangayFromDatabase == null)
                return false;

            databaseContext.Remove(barangayFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetBarangayModel>> GetAllBarangays()
        {
            List<Barangay> listOfBarangays = await databaseContext.Barangay.ToListAsync();
            return mapper.Map<List<GetBarangayModel>>(listOfBarangays);
        }

        public async Task<GetBarangayModel> GetBarangayById(int id)
        {
            Barangay? barangay = await databaseContext.Barangay.Where(b => b.BarangayId == id).FirstOrDefaultAsync();
            return barangay != null ? mapper.Map<GetBarangayModel>(barangay) : null;
        }

        public async Task<GetBarangayModel?> UpdateBarangay(UpdateBarangayModel barangay, int id)
        {
            var barangayFromDatabase = await databaseContext.Barangay.Where(b => b.BarangayId == id).FirstOrDefaultAsync();
            if (barangayFromDatabase == null)
                return null;

            barangayFromDatabase.BarangayName = barangay.BarangayName;
            barangayFromDatabase.PostalCode = barangay.PostalCode;

            await databaseContext.SaveChangesAsync();
            return mapper.Map<GetBarangayModel>(barangayFromDatabase);
        }
    }
}