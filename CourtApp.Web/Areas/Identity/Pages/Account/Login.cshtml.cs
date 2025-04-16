//using CourtApp.Application.Features.ActivityLog.Commands.AddLog;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BasePageModel<LoginModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;
        private readonly IdentityContext _identityDbContext;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager, IMediator mediator, IdentityContext _identityDbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            this._identityDbContext = _identityDbContext;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();

            string userName = Input.Email;

            if (IsValidEmail(Input.Email))
            {
                var userCheck = await _userManager.FindByEmailAsync(Input.Email);
                if (userCheck != null)
                {
                    userName = userCheck.UserName;
                }
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                _notyf.Error("Email / Username Not Found.");
                ModelState.AddModelError(string.Empty, "Email / Username Not Found.");
                return Page();
            }

            if (!user.IsActive)
                return RedirectToPage("./Deactivated");

            if (!user.EmailConfirmed)
            {
                _notyf.Error("Email Not Confirmed.");
                ModelState.AddModelError(string.Empty, "Email Not Confirmed.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                _notyf.Success($"Logged in as {userName}.");

                var userRoleNames = await _userManager.GetRolesAsync(user);
                var normalizedUserRoles = userRoleNames.Select(r => r.ToUpper()).ToList();

                var existingClaims = await _userManager.GetClaimsAsync(user);

                List<Guid> linkedIds = new();
                Guid userIdGuid = Guid.Parse(user.Id);

                if (normalizedUserRoles.Contains("LAWYER"))
                {
                    linkedIds = await _identityDbContext.LawyerUsers
                        .Where(w => w.LawyerId == user.Id)
                        .Select(s => s.Id)
                        .ToListAsync();
                }
                else //if (normalizedUserRoles.Contains("ASSOCIATE") || normalizedUserRoles.Contains("CLERK"))
                {
                    linkedIds = await _identityDbContext.LawyerUsers
                        .Where(w => w.Id == userIdGuid)
                        .Select(s => Guid.Parse(s.LawyerId))
                        .ToListAsync();
                }

                linkedIds.Add(userIdGuid);
                string lawyerIdsCsv = string.Join(",", linkedIds);

                var allClaims = new List<Claim>(existingClaims)
                                {
                                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                                    new Claim(ClaimTypes.Name, user.UserName),
                                    new Claim("LinkedIds", lawyerIdsCsv)
                                };

                await _signInManager.SignOutAsync(); // Ensures a clean sign-in context

                var identity = new ClaimsIdentity(allClaims, IdentityConstants.ApplicationScheme); // ✅ Use correct scheme
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }

            if (result.IsLockedOut)
            {
                _notyf.Warning("User account locked out.");
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            _notyf.Error("Invalid login attempt.");
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}