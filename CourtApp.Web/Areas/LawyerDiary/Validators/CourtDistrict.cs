﻿using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class CourtDistrict : AbstractValidator<CourtDistrictViewModel>
    {
        public CourtDistrict()
        {
            RuleFor(p => p.Name_En)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                ;
            RuleFor(p => p.Abbreviation)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(4).WithMessage("{PropertyName} must not exceed 4 characters.")
                ;
        }
    }
}
