using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Entities;
using System;
using System.Linq;

namespace HotelsApi.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionModel>
    {
        public CreateTransactionValidator(DatabaseContext databaseContext)
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Must(hotelId => databaseContext.Hotels.Any(h => h.HotelId == hotelId))
                .WithMessage("HotelId must be valid.");

            RuleFor(x => x.HotelName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.HotelCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.DateFrom)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .LessThan(x => x.DateTo).WithMessage("DateFrom must be earlier than DateTo.");

            RuleFor(x => x.DateTo)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("{PropertyName} must be a valid phone number.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");
        }
    }
}