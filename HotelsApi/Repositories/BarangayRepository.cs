using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface IBarangayRepository
    {
        Task<List<Barangay>> GetAllBarangays();
        Task<Barangay> GetBarangayById(int id);
        Task<Barangay> CreateBarangay(Barangay barangay);
        Task<Barangay?> UpdateBarangay(Barangay barangay, int id);
        Task<bool> DeleteBarangay(int id);
    }

    public class BarangayRepository : IBarangayRepository
    {
        public DatabaseContext databaseContext;

        public BarangayRepository(DatabaseContext databaseContext) 
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Barangay> CreateBarangay(Barangay barangay)
        {
            await databaseContext.Barangay.AddAsync(barangay);
            await databaseContext.SaveChangesAsync();
            return barangay;
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

        public async Task<List<Barangay>> GetAllBarangays()
        {
            List<Barangay> listOfBarangays = await databaseContext.Barangay.ToListAsync();
            return listOfBarangays;
        }

        public async Task<Barangay> GetBarangayById(int id)
        {
            Barangay? barangay = await databaseContext.Barangay.Where(b => b.BarangayId == id).FirstOrDefaultAsync();

            if (barangay != null)
                return barangay;

            return null;
        }

        public async Task<Barangay?> UpdateBarangay(Barangay barangay, int id)
        {
            var barangayFromDatabase = await databaseContext.Barangay.Where(b => b.BarangayId == id).FirstOrDefaultAsync();

            if (barangayFromDatabase == null)
                return null;

            barangayFromDatabase.BarangayName = barangay.BarangayName;
            barangayFromDatabase.PostalCode = barangay.PostalCode;
            barangayFromDatabase.CityId = barangay.CityId;

            await databaseContext.SaveChangesAsync();
            return barangayFromDatabase;
        }
    }
}