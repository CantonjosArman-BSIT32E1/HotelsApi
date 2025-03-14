using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllStates();
        Task<State> GetStateById(int id);
        Task<State> CreateState(State state);
        Task<State?> UpdateState(State state, int id);
        Task<bool> DeleteState(int id);
    }

    public class StateRepository : IStateRepository
    {
        public DatabaseContext databaseContext;

        public StateRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<State> CreateState(State state)
        {
            await databaseContext.State.AddAsync(state);
            await databaseContext.SaveChangesAsync();
            return state;
        }

        public async Task<bool> DeleteState(int id)
        {
            var stateFromDatabase = await databaseContext.State.Where(s => s.StateId == id).FirstOrDefaultAsync();

            if (stateFromDatabase == null)
                return false;

            databaseContext.Remove(stateFromDatabase);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<State>> GetAllStates()
        {
            List<State> listOfStates = await databaseContext.State.ToListAsync();
            return listOfStates;
        }

        public async Task<State> GetStateById(int id)
        {
            State? state = await databaseContext.State.Where(s => s.StateId == id).FirstOrDefaultAsync();

            if (state != null)
                return state;

            return null;
        }

        public async Task<State?> UpdateState(State state, int id)
        {
            var stateFromDatabase = await databaseContext.State.Where(s => s.StateId == id).FirstOrDefaultAsync();

            if (stateFromDatabase == null)
                return null;

            stateFromDatabase.StateCode = state.StateCode;
            stateFromDatabase.StateName = state.StateName;


            await databaseContext.SaveChangesAsync();
            return stateFromDatabase;
        }
    }
}
