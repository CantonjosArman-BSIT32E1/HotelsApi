using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.Validators
{
    public class CreateBarangayValidator : AbstractValidator<CreateBarangayModel>
    {
        public CreateBarangayValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.BarangayName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(name => !databaseContext.Barangay.Any(b => b.BarangayName == name))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(cityId => databaseContext.City.Any(c => c.CityId == cityId))
                .WithMessage("CityId must be valid.");
        }
    }
}
