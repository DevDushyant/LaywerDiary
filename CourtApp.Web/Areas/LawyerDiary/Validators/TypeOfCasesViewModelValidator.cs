using CourtApp.Web.Areas.LawyerDiary.Models;
using FluentValidation;
namespace CourtApp.Web.Areas.LawyerDiary.Validators
{
    public class TypeOfCasesViewModelValidator : AbstractValidator<CaseType>
    {
        public TypeOfCasesViewModelValidator()
        {
            RuleFor(p => p.Name_En)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Abbreviation)
            .NotEmpty().WithMessage("Please enter abbreviation.")
            .NotNull()
            .Matches(@"^(?!'+$)[a-zA-Z']+(?:\s+[a-zA-Z']+)*$").WithMessage("Special charector is not allowed!");

        }
    }
}
