﻿using FluentValidation;
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
                .NotNull().NotEmpty().WithMessage("{PropertyName} should not be null.")
                .Must(BarangayName => databaseContext.Barangay.Where(a => a.BarangayName == BarangayName).FirstOrDefault() == null)
                    .WithMessage("{PropertyName} must be unique.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .NotNull();

        }
    }
}
