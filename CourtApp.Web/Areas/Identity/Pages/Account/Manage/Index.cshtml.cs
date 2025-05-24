using AuditTrail.Abstrations;
using CourtApp.Domain.Entities.LawyerDiary;
using CourtApp.Infrastructure.Identity.Models;
using CourtApp.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourtApp.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList Specializations { get; set; }
        public SelectList Years { get; set; }
        public SelectList States { get; set; }
        public SelectList CourtTypes { get; set; }
        public SelectList CourtDistricts { get; set; }
        public SelectList Complexes { get; set; }
        public SelectList Courts { get; set; }
        public SelectList Genders { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Username")]
            public string Username { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
            public string EnrollmentNo { get; set; }
            public string Mobile { get; set; }

            [Display(Name ="Date of birth")]
            public DateTime DateOfBirth { get; set; }

            [Display(Name = "Date of joining")]
            public DateTime DateOfJoining { get; set; }

            public ProfessionalInfo ProfInfo { get; set; }
            public WorkLocation WorkLocInfo { get; set; }
            public AddressInfo AddressInfo { get; set; }

        }

        public class AddressInfo
        {
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public int CityId { get; set; }
            [Display(Name ="Complete Address")]
            public string Address { get; set; }
            public string Landmark { get; set; }
        }

        public class WorkLocation
        {
            public string Address { get; set; }
            public int StateId { get; set; }
            public int DistrictId { get; set; }
            public Guid CourtTypeId { get; set; }
            public Guid CourtId { get; set; }
        }

        public class ContactInfo
        {
            public string O_Phone { get; set; }
            public string R_Phone { get; set; }
            public string P_Email { get; set; }
            public string O_Email { get; set; }
        }

        public class ProfessionalInfo
        {
            [Display(Name ="Enrollment Number")]
            public string EnrollmentNo { get; set; }            
            
            [Display(Name = "Bar Association Number")]
            public string BarAssociationNumber { get; set; }

            [Display(Name = "License Date")]
            public DateTime PracticeLicenseDate { get; set; }

            [Display(Name = "Practice Since")]
            public int PracticeSince { get; set; }

            [Display(Name = "Specialization Date")]
            public Guid SpecializationId { get; set; }
            
        }


        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var profilePicture = user.ProfilePicture;
            Username = userName;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Username = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicture = profilePicture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var firstName = user.FirstName;
            var lastName = user.LastName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                user.ProfilePicture = file.OptimizeImageSize(720, 720);
                await _userManager.UpdateAsync(user);
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}