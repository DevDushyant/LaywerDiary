using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourtApp.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EnrollmentNo { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
    }
}