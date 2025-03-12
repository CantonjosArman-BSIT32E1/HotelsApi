using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.Validators
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryModel>
    {
        public CreateCountryValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.CountryName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(name => !databaseContext.Country.Any(c => c.CountryName == name))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(code => !databaseContext.Country.Any(c => c.CountryCode == code))
                .WithMessage("{PropertyName} must be unique.");
        }
    }
}
