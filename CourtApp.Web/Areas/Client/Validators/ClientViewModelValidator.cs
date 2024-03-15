using CourtApp.Web.Areas.Client.Model;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CourtApp.Web.Areas.Client.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.LastName)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();

            RuleFor(p => p.FatherName)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            //RuleFor(p => p.Mobile)
            // .NotEmpty().WithMessage("{PropertyName} is required.")
            // .NotNull();
             //.MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.");         
             //.Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("{PropertyName} not valid");



        }
    }
}
