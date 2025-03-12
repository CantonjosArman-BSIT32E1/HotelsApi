using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;

namespace HotelsApi.Validators
{
    public class CreateHotelValidator : AbstractValidator<CreateHotelModel>
    {
        public CreateHotelValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.HotelName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(name => !databaseContext.Hotels.Any(h => h.HotelName == name))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.HotelCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(code => !databaseContext.Hotels.Any(h => h.HotelCode == code))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.HotelDescription)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.BarangayId)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(barangayId => databaseContext.Barangay.Any(b => b.BarangayId == barangayId))
                .WithMessage("BarangayId must be valid.");
        }
    }
}
