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
    public interface IStateService
    {
        Task<List<GetStateModel>> GetAllStates();
        Task<GetStateModel> GetStateById(int id);
        Task<GetStateModel> CreateState(CreateStateModel state);
        Task<GetStateModel?> UpdateState(UpdateStateModel state, int id);
        Task<bool> DeleteState(int id);
    }

    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;
        private readonly DatabaseContext databaseContext;
        private readonly IMapper mapper;

        public StateService(DatabaseContext databaseContext, IStateRepository stateRepository, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.stateRepository = stateRepository;
            this.mapper = mapper;
        }

        public async Task<GetStateModel> CreateState(CreateStateModel state)
        {
            CreateStateValidator validator = new CreateStateValidator(databaseContext);
            ValidationResult results = validator.Validate(state);

            if (results.Errors.Count > 0)
            {
                throw new Exception(string.Join(",", results.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var createStateResult = await stateRepository.CreateState(mapper.Map<State>(state));
            return mapper.Map<GetStateModel>(createStateResult);
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

        public async Task<List<GetStateModel>> GetAllStates()
        {
            List<State> listOfStates = await databaseContext.State.ToListAsync();
            return mapper.Map<List<GetStateModel>>(listOfStates);
        }

        public async Task<GetStateModel> GetStateById(int id)
        {
            State? state = await databaseContext.State.Where(s => s.StateId == id).FirstOrDefaultAsync();
            return state != null ? mapper.Map<GetStateModel>(state) : null;
        }

        public async Task<GetStateModel?> UpdateState(UpdateStateModel state, int id)
        {
            var stateFromDatabase = await databaseContext.State.Where(s => s.StateId == id).FirstOrDefaultAsync();
            if (stateFromDatabase == null)
                return null;

            stateFromDatabase.StateCode = state.StateCode;
            stateFromDatabase.StateName = state.StateName;

            await databaseContext.SaveChangesAsync();
            return mapper.Map<GetStateModel>(stateFromDatabase);
        }
    }
}