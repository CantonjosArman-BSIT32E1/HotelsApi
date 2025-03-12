using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.Validators
{
    public class CreateCityValidator : AbstractValidator<CreateCityModel>
    {
        public CreateCityValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.CityName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(name => !databaseContext.City.Any(c => c.CityName == name))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.CityCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.StateId)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(stateId => databaseContext.State.Any(s => s.StateId == stateId))
                .WithMessage("StateId must be valid.");
        }
    }
}
