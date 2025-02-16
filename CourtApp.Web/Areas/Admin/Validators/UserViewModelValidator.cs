using CourtApp.Web.Areas.Admin.Models;
using FluentValidation;

namespace CourtApp.Web.Areas.Admin.Validators
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Valid Email is required.");

            // Conditional Validation: EnrollmentNo is required only if UserType is "Lawyer"
            RuleFor(x => x.EnrollmentNo)
                .NotEmpty().WithMessage("Enrollment Number is required for Lawyers.")
                .When(x => x.Role == "LAWYER");

        }
    }
}
