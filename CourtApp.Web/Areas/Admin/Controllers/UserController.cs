using CourtApp.Application.Constants;
using CourtApp.Application.DTOs.Mail;
using CourtApp.Application.Enums;
using CourtApp.Application.Interfaces.Shared;
using CourtApp.Infrastructure.DbContexts;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Abstractions;
using CourtApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _identityDbContext;
        private readonly IMailService _emailSender;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IdentityContext _identityDbContext,
            IMailService emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this._identityDbContext = _identityDbContext;
            _emailSender = emailSender;
        }

        [Authorize(Policy = Permissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var Roles = await _userManager.GetRolesAsync(currentUser);
            if (Roles.Contains("SuperAdmin"))
            {
                var allUsersExceptCurrentUser = await _userManager.Users
                    .Where(a => a.Id != currentUser.Id && a.UserType != "Operator").ToListAsync();
                var model = _mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);
                return PartialView("_ViewAll", model);
            }
            var OperDt = _identityDbContext.Operators.Where(w => w.LawyerId.Equals(currentUser.Id)).ToList();
            var OprUser = await _userManager.Users
                    .Where(a => a.Id != currentUser.Id && a.UserType == "Operator")
                    .ToListAsync();
            var fnlRs = from ou in OprUser
                        join od in OperDt on ou.Id equals od.Id.ToString()
                        select new OperatorViewModel
                        {
                            FullName = ou.FirstName + " " + ou.LastName,
                            Email = ou.Email,
                            Mobile = ou.Mobile,
                            EmailConfirmed = ou.EmailConfirmed,
                            ProfilePicture = ou.ProfilePicture,
                            Id = od.Id,
                            IsActive = ou.IsActive,
                            DateOfJoining = od.DateOfJoining
                        };
            return PartialView("_Operator", fnlRs);
        }

        public async Task<IActionResult> OnGetCreate(Guid id)
        {
            if (id == Guid.Empty)
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", new UserViewModel()) });
            else
            {
                var OprUser = await _userManager.Users
                    .Where(a => a.UserType == "Operator" && a.Id == id.ToString())
                    .FirstOrDefaultAsync();
                var model = _mapper.Map<IEnumerable<UserViewModel>>(OprUser);
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", new UserViewModel()) });
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreate(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Get Current Lawyer
                var lawyer = await _userManager.GetUserAsync(User);
                if (lawyer == null && lawyer.UserType != "Lawyer") return Unauthorized();
                // Check if operator has already registered email
                var existingUser = await _userManager.FindByEmailAsync(userModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "An account with this email already exist");
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", new UserViewModel()) });
                }
                MailAddress address = new MailAddress(userModel.Email);
                string userName = address.User;
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Mobile = userModel.Mobile,
                    Gender = userModel.Gender,
                    DateOfBirth = userModel.DateOfBirth,
                    UserType = "Operator"
                };
                var result = await _userManager.CreateAsync(user, "123Pa$$word!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Operator.ToString());
                    //Link operator to the lawyer
                    var nwopr = new OperatorUser
                    {
                        Id = Guid.Parse(user.Id),
                        LawyerId = lawyer.Id,
                        DateOfJoining = userModel.DateOfJoining,
                        AddressInfo = new AddressInfo
                        {
                            StateId = 0,
                            CityId = 0,
                            DistrictId = 0,
                            StreetAddress = userModel.Address
                        },
                        CreatedBy = user.Id,
                        CreatedOn = DateTime.Now,
                    };
                    _identityDbContext.Operators.Add(nwopr);
                    await _identityDbContext.SaveChangesAsync();
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string returnUrl = Url.Content("~/");
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    var mailRequest = new MailRequest
                    {
                        Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                        From = "info@sparo.com",
                        To = userModel.Email,
                        Subject = "Please verify your email address\r\n"
                    };
                    await _emailSender.SendAsync(mailRequest);
                    _notify.Success($"Kindly confirm the email");
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    _notify.Error(error.Description);
                }
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", userModel);
                return new JsonResult(new { isValid = false, html = html });
            }
            return default;
        }
    }
}