using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.Validators
{
    public class CreateStateValidator : AbstractValidator<CreateStateModel>
    {
        public CreateStateValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.StateName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(name => !databaseContext.State.Any(s => s.StateName == name))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.StateCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(countryId => databaseContext.Country.Any(c => c.CountryId == countryId))
                .WithMessage("CountryId must be valid.");
        }
    }
}
